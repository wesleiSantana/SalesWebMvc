using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _service;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService service, DepartmentService departmentService)
        {
            this._service = service;
            this._departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = this._service.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = this._departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments, }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            this._service.Insert(seller);
            return RedirectToAction(nameof(Index));
        }


    }
}