using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bs.LicenseManager.Core.Service;
using bs.LicenseManager.Core.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bs.LicenseManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly LicenseService licenseService;

        public CustomerController(LicenseService licenseService)
        {
            this.licenseService = licenseService;
        }
        [HttpGet("customers")]
        public async Task<IActionResult> GetCustomers()
        {
            return Ok(await licenseService.GetCustomersIndexView());
        }

        [HttpGet("customer")]
        public async Task<IActionResult> GetCustomer(string customerId)
        {
            return Ok(new { test = "Get Customer " + customerId });
        }

        [HttpPatch("customer")]
        public async Task<IActionResult> UpdateCustomer(CustomerViewModel model)
        {
            return Ok(new { test = "Update Customer " + model.Id });
        }

        [HttpPost("customer")]
        public async Task<IActionResult> CreateCustomer(CustomerViewModel model)
        {
            return Ok(await licenseService.CreateCustomer(model.Name, model.EmailContact));

        }

        [HttpDelete("customer")]
        public async Task<IActionResult> DeleteCustomer(string customerId)
        {
            return Ok(new { test = "Delete Customer " + customerId });

        }
    }
}