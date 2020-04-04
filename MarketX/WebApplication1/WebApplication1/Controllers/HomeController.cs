using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MarketX.Models;
using MarketX.Data;

namespace MarketX.Controllers
{
    public class HomeController : Controller
    {
        private readonly MarketXContext context;

        public HomeController(MarketXContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChooseCategory(string CategoryName)
        {
            var props = context.CategoryProperties.Where(cp => cp.Category.Name == CategoryName).Select(cp => cp.Property).ToList();
            return PartialView("_Properties", props);
        }

        public IActionResult ChooseCounty(string CountyName)
        {
            var cities = context.Counties.Where(c => c.Name == CountyName).Select(c => c.Cities).FirstOrDefault();
            return PartialView("/Views/Shared/Components/CityCountySelect/_CitySelect.cshtml", cities);
        }
    }
}
