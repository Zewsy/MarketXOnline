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
            var priorizedAdvertisements = context.Advertisements.Where(a => a.IsPriorized).Take(8).ToList();
            var otherAdvertisements = context.Advertisements.Where(a => !a.IsPriorized)
                                                            .OrderByDescending(a => a.CreatedDate)
                                                            .Take(8).ToList();
            var advertisements = priorizedAdvertisements.Concat(otherAdvertisements);
            return View(advertisements);
        }
    }
}
