using Microsoft.VisualStudio.TestTools.UnitTesting;
using bs.LicenseManager.Core.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using bs.Data;
using bs.Data.Interfaces;
using bs.LicenseManager.Core.Repository;
using System.Threading.Tasks;

namespace bs.LicensesManager.Core.Service.Tests
{
    [TestClass()]
    public class LicenseServiceTests
    {
        private IServiceProvider serviceProvider;
        private IServiceCollection services;

        [TestInitialize]
        public void Init()
        {
            services = new ServiceCollection();
            services.AddBsData(new DbContext
            {
                ConnectionString = "Data Source=.\\bs.LicenseManager.Test.db;Version=3;BinaryGuid=False;",
                DatabaseEngineType = DbType.SQLite,
                Create = true,
                Update = true,
                LookForEntitiesDllInCurrentDirectoryToo = false
            });
            services.AddScoped<LicenseRepository>();
            services.AddScoped<LicenseService>();
            serviceProvider = services.BuildServiceProvider();
        }


        [TestMethod()]
        public void CreateCustomerTest()
        {
            var licenseService = serviceProvider.GetService<LicenseService>();
            var customerId = licenseService.CreateCustomer("bSoft", "fcavallari@bsoftsolutions.it");
        }

        [TestMethod()]
        public void CompleteWorkFlowTest()
        {
            Task.Run(async () => {
                var licenseService = serviceProvider.GetService<LicenseService>();
                var customerId = await licenseService.CreateCustomer("bSoft", "fcavallari@bsoftsolutions.it");
                var productId = await licenseService.CreateProduct("License Manager", "Applicazione per la gestione delle licenze");
                var versionId = await licenseService.CreateVersion(productId, "Versione 1.0.0.0");
                var feature1Id = await licenseService.CreateProductFeature(productId, "ModuleOne");
                var feature2Id = await licenseService.CreateProductFeature(productId, "ModuleTwo");
                var licenseTokenId = await licenseService.CreateLicense(versionId, customerId, DateTime.UtcNow.AddDays(30), Standard.Licensing.LicenseType.Standard);
                var feature1ValueId = licenseService.CreateLicenseTokenFeature(licenseTokenId, feature1Id, "true");
                var feature2ValueId = licenseService.CreateLicenseTokenFeature(licenseTokenId, feature2Id, "false");
                await licenseService.EmitLicense(licenseTokenId);

            }).Wait();
           
        }
    }
}


