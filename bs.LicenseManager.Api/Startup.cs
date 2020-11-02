using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bs.Data;
using bs.Data.Interfaces;
using bs.LicenseManager.Core.Mapping;
using bs.LicenseManager.Core.Repository;
using bs.LicenseManager.Core.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace bs.LicenseManager.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(option => {
                option.AddPolicy("cors_policy", builder => builder.AllowAnyOrigin());
            });
            services.AddBsData(new DbContext
            {
                ConnectionString = "Data Source=.\\bs.LicenseManager.Test.db;Version=3;BinaryGuid=False;",
                DatabaseEngineType = DbType.SQLite,
                Create = false,
                Update = true,
                LookForEntitiesDllInCurrentDirectoryToo = false
            });
            services.AddScoped<LicenseRepository>();
            services.AddScoped<LicenseService>();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("cors_policy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
