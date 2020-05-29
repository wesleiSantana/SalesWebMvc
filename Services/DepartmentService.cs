using SalesWebMvc.Models;
using System.Linq;
using System.Collections.Generic;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context)
        {
            this._context = context;
        }

        public List<Department> FindAll()
        {
            return this._context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}