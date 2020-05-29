using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _service;

        public SellersController(SellerService service)
        {
            this._service = service;
        }

        public IActionResult Index()
        {
            var list = this._service.FindAll();
            return View(list);
        }
    }
}