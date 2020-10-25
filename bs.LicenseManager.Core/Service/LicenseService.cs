using AutoMapper;
using bs.Data.Helpers;
using bs.Data.Interfaces;
using bs.LicensesManager.Core.Model;
using bs.LicensesManager.Core.Repository;
using bs.LicensesManager.Core.ViewModel;
using Standard.Licensing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bs.LicensesManager.Core.Service
{
    public class LicenseService
    {
        private static string passPhrase = "Sono un frase di prova tanto per fare qualche chiave di prova";
        private readonly LicenseRepository licenseRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public LicenseService(IUnitOfWork unitOfWork, IMapper mapper, LicenseRepository licenseRepository)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.licenseRepository = licenseRepository;
        }

        public async Task<Guid> CreateCustomer(string name, string emailContact)
        {
            return await unitOfWork.RunInTransactionAsync(async () =>
            {
                var entity = new CustomerEntity
                {
                    Active = true,
                    Name = name,
                    EmailContact = emailContact
                };

                await licenseRepository.CreateCustomer(entity);

                return entity.Id;
            });
        }

        public async Task<Guid> CreateLicense(Guid productVersionId, Guid customerId, DateTime expireDate, LicenseType licenseType)
        {
            return await unitOfWork.RunInTransactionAsync(async () =>
            {
                var productVersion = await licenseRepository.GetProductVersion(productVersionId);
                var customer = await licenseRepository.GetCustomer(customerId);

                // Create entity
                var entity = new LicenseTokenEntity
                {
                    Active = true,
                    Customer = customer,
                    EmitDate = DateTime.UtcNow,
                    ExpireDate = expireDate,
                    LicenseType = licenseType,
                    PassPhrase = passPhrase,
                    PrivateKey = "*",
                    PublicKey = "*",
                    LicFileContent = "*",
                    Version = productVersion,
                };

                await licenseRepository.CreateLicenseToken(entity);

                // update the product
                productVersion.Product.LicenseTokens.Add(entity);
                await licenseRepository.UpdateProduct(productVersion.Product);

                return entity.Id;
            });
        }

        public async Task<CustomerViewModel> GetCustomer(string id)
        {
            return await unitOfWork.RunInTransactionAsync(async () =>
            {
                return mapper.Map<CustomerViewModel>(await licenseRepository.GetCustomer(Guid.Parse(id)));
            });
        }

        public async  Task UpdateCustomer(string id, string name, string emailContact, bool active)
        {
            await unitOfWork.RunInTransactionAsync(async () =>
            {
                var entity = await licenseRepository.GetCustomer(Guid.Parse(id));
                entity.Active = active;
                entity.Name = name;
                entity.EmailContact = emailContact;

                await licenseRepository.UpdateCustomer(entity);
            });
        }

        public async Task<Guid> CreateLicenseTokenFeature(Guid licenseTokenId, Guid productFeatureId, string featureValue)
        {
            return await unitOfWork.RunInTransactionAsync(async () =>
            {
                var licenseToken = await licenseRepository.GetLicenseToken(licenseTokenId);
                var productFeature = await licenseRepository.GetProductFeature(productFeatureId);

                var entity = new LicenseTokenFeatureEntity
                {
                    Active = true,
                    ProductFeature = productFeature,
                    LicenseToken = licenseToken,
                    Value = featureValue
                };

                await licenseRepository.CreateLicenseTokenFeature(entity);

                // Update license tocken
                licenseToken.LicenseTokenFeatures.Add(entity);
                await licenseRepository.UpdateLicenseToken(licenseToken);

                return entity.Id;
            });
        }

        public async Task<Guid> CreateProduct(string name, string description)
        {
            return await unitOfWork.RunInTransactionAsync(async () =>
            {
                var entity = new ProductEntity
                {
                    Active = true,
                    Description = description,
                    Name = name
                };

                await licenseRepository.CreateProduct(entity);

                return entity.Id;
            });
        }

        public async Task<Guid> CreateProductFeature(Guid productId, string featureKey)
        {
            return await unitOfWork.RunInTransactionAsync(async () =>
            {
                var product = await licenseRepository.GetProduct(productId);

                var entity = new ProductFeatureEntity
                {
                    Active = true,
                    Key = featureKey,
                    Product = product
                };

                await licenseRepository.CreateProductFeature(entity);

                product.Features.Add(entity);

                await licenseRepository.UpdateProduct(product);

                return entity.Id;
            });
        }

        public async Task<Guid> CreateVersion(Guid productId, string version)
        {
            return await unitOfWork.RunInTransactionAsync(async () =>
            {
                var product = await licenseRepository.GetProduct(productId);

                var entity = new ProductVersionEntity
                {
                    Active = true,
                    Product = product,
                    Version = version,
                };

                await licenseRepository.CreateVersion(entity);

                product.Versions.Add(entity);

                await licenseRepository.UpdateProduct(product);

                return entity.Id;
            });
        }

        public async Task EmitLicense(Guid licenseTokenId)
        {
            await unitOfWork.RunInTransactionAsync(async () =>
            {
                var licenseToken = await licenseRepository.GetLicenseToken(licenseTokenId);

                // Generate key pair
                var keyGenerator = Standard.Licensing.Security.Cryptography.KeyGenerator.Create();
                var keyPair = keyGenerator.GenerateKeyPair();
                licenseToken.PrivateKey = keyPair.ToEncryptedPrivateKeyString(passPhrase);
                licenseToken.PublicKey = keyPair.ToPublicKeyString();

                // Convert features in dictionary
                var features = new Dictionary<string, string>();
                foreach (var ltf in licenseToken.LicenseTokenFeatures)
                {
                    features.Add(ltf.ProductFeature.Key, ltf.Value);
                }

                // Generate license
                var license = License.New()
                    .WithUniqueIdentifier(licenseToken.Id)
                    .As(licenseToken.LicenseType)
                    .ExpiresAt(licenseToken.ExpireDate)
                    //.WithMaximumUtilization(5)
                    .WithProductFeatures(features)
                    .LicensedTo(licenseToken.Customer.Name, licenseToken.Customer.EmailContact)
                    .CreateAndSignWithPrivateKey(licenseToken.PrivateKey, licenseToken.PassPhrase);

                licenseToken.LicFileContent = license.ToString();
                await licenseRepository.UpdateLicenseToken(licenseToken);
            });
        }

        public async Task<CustomersViewModel> GetCustomersView()
        {
            return await unitOfWork.RunInTransactionAsync(async () =>
            {
                var customers = await licenseRepository.GetCustomers();
                return new CustomersViewModel()
                {
                    CustomersList = mapper.Map<IList<CustomerViewModel>>(customers)
                };
            });
        }
    }
}