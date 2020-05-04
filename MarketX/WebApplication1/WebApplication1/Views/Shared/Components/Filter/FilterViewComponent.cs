using MarketX.DAL;
using MarketX.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketX.BLL.Interfaces;
using MarketX.BLL.DTOs;
using MarketX.ViewModels;

namespace MarketX.Views.Shared.Filter
{
    public class FilterViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public FilterViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(SearchModel actualSearchModel)
        {
            var categories = await _categoryService.GetMainCategoriesAsync();

            SearchModelWithMetadata model = new SearchModelWithMetadata(actualSearchModel) {PropertyInputs = actualSearchModel.PropertyInputs, MainCategories = categories.ToList() };

            return View("Filter", model);
        }
    }
}
