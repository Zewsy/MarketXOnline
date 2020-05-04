using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketX.BLL.Interfaces;
using MarketX.ViewModels;
using MarketX.BLL.DTOs;

namespace MarketX.Views.Shared.Components.DetailedSearch
{
    public class SearchViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public SearchViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool ShowDetailedSearch)
        {
            var categories = await _categoryService.GetMainCategoriesAsync();
            SearchModelWithMetadata model = new SearchModelWithMetadata() { MainCategories = categories.ToList(), IsDetailed = ShowDetailedSearch };

            return View("Search", model);
        }
    }
}
