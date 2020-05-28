using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SalesWebMvc.Models;

namespace SalesWebMvc.Controllers
{
    public class DepartmentsController : Controller 
    {
        public IActionResult Index()
        {
            List<Department> departments = new List<Department>();
            departments.Add(new Department() { Id = 1, Name = "Eletronicos" });
            departments.Add(new Department() { Id = 2, Name = "Casa mesa e banho" });
            departments.Add(new Department() { Id = 3, Name = "Roupas" });

            return View(departments);
        }
    }
}