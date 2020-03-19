using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketX.Data;
using Microsoft.AspNetCore.Mvc;

namespace MarketX.Controllers
{
    [Route("Advertisement")]
    public class AdvertisementController : Controller
    {
        private readonly MarketXContext context;
        public AdvertisementController(MarketXContext _context)
        {
            context = _context;
        }


        [HttpGet("{id}")]
        public IActionResult Advertisement(int id)
        {
            var advertisement = context.Advertisements.Where(a => a.ID == id).FirstOrDefault();

            if(advertisement != null)
                return View(advertisement);

            return NotFound();
        }
    }
}