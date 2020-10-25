using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bs.LicensesManager.Core.Model;
using bs.LicensesManager.Core.Service;
using bs.LicensesManager.Core.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bs.LicencesManager.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IMapper mapper;
        private readonly LicenseService licenseService;

        public CustomerController(IMapper mapper,LicenseService licenseService)
        {
            this.mapper = mapper;
            this.licenseService = licenseService;
        }
        // GET: Customer
        public async Task<ActionResult> Index()
        {
           return View(await licenseService.GetCustomersView());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customerId = await licenseService.CreateCustomer(model.Name, model.EmailContact);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: Customer/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            return View(await licenseService.GetCustomer(id));
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                await licenseService.UpdateCustomer(model.Id, model.Name, model.EmailContact, model.Active);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}