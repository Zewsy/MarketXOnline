using MarketX.Data;
using MarketX.Models;
using MarketX.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Views.Shared.Components.DetailedSearch
{
    public class SearchViewComponent : ViewComponent
    {
        private readonly MarketXContext context;

        public SearchViewComponent(MarketXContext _context)
        {
            context = _context;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool ShowDetailedSearch)
        {
            var categories = await context.Categories.Where(c => c.ParentCategoryID == null).ToListAsync();
            SearchFormModelWithMetadata model = new SearchFormModelWithMetadata() { MainCategories = categories, IsDetailed = ShowDetailedSearch };
            
            return View("Search", model);
        }
    }
}
