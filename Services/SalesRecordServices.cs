//system
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
//microsoft
using Microsoft.EntityFramewrokCore;
//models
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SalesRecordServices
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordServices(SalesWebMvcContext context)
        {
            this._context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in this._context.SalesRecord select obj;
            if(minDate.HasValue) result = result.Where(x => x.Date >= minDate.Value);

            if(maxDate.HasValue) result = result.Where(x => x.Date <= maxDate.Value);

            return await result.Include(x => x.Seller).Include(x => x.Seller.Department).OrderByDescending(x => x.Date).ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in this._context.SalesRecord select obj;
            if(minDate.HasValue) result = result.Where(x => x.Date >= minDate.Value);

            if(maxDate.HasValue) result = result.Where(x => x.Date <= maxDate.Value);

            return await result
            .Include(x => x.Seller)
            .Include(x => x.Seller.Department)
            .OrderByDescending(x => x.Date)
            .GroupBy(x => x.Seller.Department)
            .ToListAsync();
        }
    }
}