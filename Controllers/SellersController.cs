using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using System.Collections.Generic;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _serviceService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService serviceService, DepartmentService departmentService)
        {
            this._serviceService = serviceService;
            this._departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = this._serviceService.FindAll();
            return View(list);
        } 

        public IActionResult Create()
        {
            var departments = this._departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            this._serviceService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            var obj = this._serviceService.FindById(id.Value);
            if (obj == null) return RedirectToAction(nameof(Error), new { message = "Id not found." });

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            this._serviceService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            var obj = this._serviceService.FindById(id.Value);
            if (obj == null) return RedirectToAction(nameof(Error), new { message = "Id not found." });

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            var obj = this._serviceService.FindById(id.value);
            if (obj == null) return RedirectToAction(nameof(Error), new { message = "Id not found." });

            List<Department> departments = this._departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { SellerService = obj, departments = departments };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id) return RedirectToAction(nameof(Error), new { message = "Id mismatch" });

            try
            {
                this._serviceService.Update(seller);
                return RedirectToAction(nameof("Index"));
            }
            catch (ApplicationException error)
            {
                return RedirectToAction(nameof(Error), new { message = error.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}