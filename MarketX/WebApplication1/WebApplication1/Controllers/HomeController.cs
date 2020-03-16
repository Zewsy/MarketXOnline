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
    }
}
