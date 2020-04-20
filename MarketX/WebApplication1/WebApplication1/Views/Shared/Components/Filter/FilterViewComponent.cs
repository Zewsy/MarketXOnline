using MarketX.Data;
using MarketX.Models;
using MarketX.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Views.Shared.Filter
{
    public class FilterViewComponent : ViewComponent
    {
        private readonly MarketXContext context;
        public FilterViewComponent(MarketXContext _context)
        {
            context = _context;
        }

        public async Task<IViewComponentResult> InvokeAsync(SearchFormModel actualModelForSearch)
        {
            var categories = await context.Categories.Where(c => c.ParentCategoryID == null).ToListAsync();
            var properties = await context.CategoryProperties.Where(cp => cp.Category.Name == actualModelForSearch.Category).Select(cp => cp.Property).OrderBy(p => p.ValueType).ToListAsync();
            SearchFormModelWithMetadata model = new SearchFormModelWithMetadata(actualModelForSearch) {Properties = properties, MainCategories = categories };

            return View("Filter", model);
        }
    }
}
