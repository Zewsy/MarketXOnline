using MarketX.BLL.Interfaces;
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
        private readonly ICategoryService _categoryService;

        public CategoryTreeViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? chosenCategoryId, bool isRequired = false)
        {
            var Categories = await _categoryService.GetCategoriesAsync();
            return View("CategoryTree", new CategoryListWithChosenCategory() { Categories = Categories.ToList(), ChosenCategoryId = chosenCategoryId, IsRequired = isRequired});
        }
    }
}
