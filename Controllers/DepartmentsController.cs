using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Controllers
{
    public class DepartmentsController : Controller 
    {
        private readonly SalesWebMvcContext _context;

        public DepartmentsController(SalesWebMvcContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await this._context.Department.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null) return NotFound();

            var department = await this._context.Department.FirstOrDefaultAsync(m => m.Id == id);
            if(department == null) return NotFound();

            return View(department);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name")] Department department)
        {
            if(ModelState.IsValid) 
            {
                this._context.Add(department);
                await this._context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }       
    }
}