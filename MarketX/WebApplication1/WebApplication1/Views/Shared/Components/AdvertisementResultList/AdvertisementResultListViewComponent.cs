using MarketX.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Views.Shared.Components.AdvertisementResultList
{
    public class AdvertisementResultListViewComponent : ViewComponent
    {
        private readonly MarketXContext context;
        public AdvertisementResultListViewComponent(MarketXContext _context)
        {
            context = _context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return null;
        }
    }
}
