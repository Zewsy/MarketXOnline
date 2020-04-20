using MarketX.Data;
using MarketX.Models;
using MarketX.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Views.Shared.Components
{
    public class CategoryTreeViewComponent : ViewComponent
    {
        private readonly MarketXContext context;

        public CategoryTreeViewComponent(MarketXContext _context)
        {
            context = _context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string? chosenCategoryName, bool isRequired = false)
        {
            var Categories = await context.Categories.ToListAsync();
            return View("CategoryTree", new CategoryListWithChosenCategory() { Categories = Categories, ChosenCategoryName = chosenCategoryName, IsRequired = isRequired});
        }
    }
}
