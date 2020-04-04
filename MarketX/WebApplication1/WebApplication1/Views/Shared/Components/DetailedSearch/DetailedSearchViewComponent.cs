using MarketX.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Views.Shared.Components.DetailedSearch
{
    public class DetailedSearchViewComponent : ViewComponent
    {
        private readonly MarketXContext context;
        public DetailedSearchViewComponent(MarketXContext _context)
        {
            context = _context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mainCategories = await context.Categories.Where(c => c.ParentCategory == null).ToListAsync();
            
            return View("DetailedSearch", mainCategories);
        }
    }
}
