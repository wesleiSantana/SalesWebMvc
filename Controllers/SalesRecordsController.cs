//System
using System;
using System.Threading.Tasks;
//microsoft
using Microsoft.AspNetCore.Mvc;
// model
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordServices _salesRecordServices;

        public SalesRecordsController(SalesRecordServices salesRecordServices)
        {
            this._salesRecordServices = salesRecordServices;
        }

        public IActionResult Index()
        {
            return Index();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue) minDate = new DateTime(DateTime.Now.Year, 1, 1);

            if (!mmaxDate.HasValue) maxDate = DateTime.Now;

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = minDate.Value.ToString("yyyy-MM-dd");

            var result = await this._salesRecordServices.FindByDateAsync(minDate, maxDate);
            return Index(result);
        }

        public async Task<IActionResult> GroupingSearchearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue) minDate = new DateTime(DateTime.Now.Year, 1, 1);

            if (!mmaxDate.HasValue) maxDate = DateTime.Now;

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = minDate.Value.ToString("yyyy-MM-dd");

            var result = await this._salesRecordServices.FindByDateGroupingAsync(minDate, maxDate);
            return Index(result);
        }
    }
}