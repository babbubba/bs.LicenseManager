using bs.Data.Interfaces;
using bs.LicensesManager.Core.Model;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bs.LicensesManager.Core.Repository
{
    public class LicenseRepository : bs.Data.Repository
    {
        public LicenseRepository(IUnitOfWork unitOfwork) : base(unitOfwork)
        {
        }

        internal async Task CreateCustomer(CustomerEntity entity)
        {
            await CreateAsync(entity);
        }

        internal async Task CreateLicenseToken(LicenseTokenEntity entity)
        {
            await CreateAsync(entity);
        }

        internal async Task CreateLicenseTokenFeature(LicenseTokenFeatureEntity entity)
        {
            await CreateAsync(entity);
        }

        internal async Task CreateProduct(ProductEntity entity)
        {
            await CreateAsync(entity);
        }

        internal async Task CreateProductFeature(ProductFeatureEntity entity)
        {
            await CreateAsync(entity);
        }
        internal async Task CreateVersion(ProductVersionEntity entity)
        {
            await CreateAsync(entity);
        }

        internal async Task<CustomerEntity> GetCustomer(Guid customerId)
        {
            return await GetByIdAsync<CustomerEntity>(customerId);
        }

        internal async Task<LicenseTokenEntity> GetLicenseToken(Guid licenseTokenId)
        {
            return await GetByIdAsync<LicenseTokenEntity>(licenseTokenId);
        }

        internal async Task<ProductEntity> GetProduct(Guid productId)
        {
            return await GetByIdAsync<ProductEntity>(productId);
        }

        internal async Task<ProductFeatureEntity> GetProductFeature(Guid productFeatureId)
        {
            return await GetByIdAsync<ProductFeatureEntity>(productFeatureId);
        }

        //internal async Task<ProductFeatureEntity> GetProductFeatureByKey(string featureKey)
        //{
        //    return await Query<ProductFeatureEntity>().Where(pf => pf.Key == featureKey).SingleOrDefaultAsync();
        //}
        //internal async Task<IEnumerable<ProductFeatureEntity>> GetProductFeatureByKeys(IEnumerable<string> productFeatureKeys)
        //{
        //    return await Query<ProductFeatureEntity>().Where(ltf => productFeatureKeys.Contains(ltf.Key)).ToListAsync();
        //}

        internal async Task<ProductVersionEntity> GetProductVersion(Guid productVersionId)
        {
            return await GetByIdAsync<ProductVersionEntity>(productVersionId);
        }

        internal async Task UpdateFeature(ProductFeatureEntity entity)
        {
            await UpdateAsync(entity);
        }

        internal async Task UpdateLicenseToken(LicenseTokenEntity entity)
        {
            await UpdateAsync(entity);
        }

        internal async Task UpdateProduct(ProductEntity entity)
        {
            await UpdateAsync(entity);
        }

        internal async Task UpdateVersion(ProductVersionEntity entity)
        {
            await UpdateAsync(entity);
        }

        internal async Task UpdateCustomer(CustomerEntity entity)
        {
            await UpdateAsync(entity);
        }

        internal async Task<IEnumerable<CustomerEntity>> GetCustomers()
        {
            return await Query<CustomerEntity>().ToListAsync(); 
        }
    }
}