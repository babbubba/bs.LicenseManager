using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using bs.LicenseManager.Core.ViewModel;
using bs.LicenseManager.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bs.LicenseManager.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper mapper;
        private readonly LicenseService licenseService;

        public ProductController(IMapper mapper, LicenseService licenseService)
        {
            this.mapper = mapper;
            this.licenseService = licenseService;
        }
        // GET: Product
        public async Task<ActionResult> Index()
        {
            return View(await licenseService.GetProductsIndexView());
        }

        // GET: Product/Details/5
        public async Task<ActionResult> Details(string id)
        {
            return View(await licenseService.GetProductDetailsView(id));
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entityId = await licenseService.CreateProduct( model.Name, model.Description);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: Product/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            return View(await licenseService.GetProduct(id));
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                await licenseService.UpdateProduct(model.Id, model.Name, model.Description, model.Active);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
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