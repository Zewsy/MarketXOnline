using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketX.BLL.DTOs;
using MarketX.BLL.Interfaces;
using MarketX.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketX.Controllers
{
    [Route("Admin")]
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IPropertyService _propertyService;
        private readonly IAdvertisementService _advertisementService;

        public AdminController(ICategoryService categoryService, IPropertyService propertyService, IAdvertisementService advertisementService)
        {
            _categoryService = categoryService;
            this._propertyService = propertyService;
            this._advertisementService = advertisementService;
        }
        [HttpGet("AdminPanel")]
        public IActionResult AdminPanel()
        {
            return View();
        }

        [HttpGet("NewAdvertisements")]
        public async Task<IActionResult> NewAdvertisements()
        {
            var newAds = await _advertisementService.GetNewAdvertisementsAsync();
            List<ResultAdvertisementCard> model = newAds.Select(a => new ResultAdvertisementCard
            {
                ID = a.Id,
                Title = a.Title,
                City = a.City.Name,
                County = a.City.County?.Name,
                AdType = a.Seller == null ? AdType.Buying : AdType.Selling,
                Status = a.Status,
                Condition = a.Condition,
                ImagePath = a.AdvertisementImagePaths.FirstOrDefault(),
                IsPriorized = a.IsPriorized,
                Price = a.Price,
                UserName = a.Seller == null ? a.Customer?.Email : a.Seller.Email
            }).ToList();
            return View(model);
        }

        [HttpPut("ApproveAdvertisement/{id}")]
        public async Task ApproveAdvertisement(int id)
        {
            await _advertisementService.ApproveAdvertisementAsync(id);
        }

        [HttpGet("EditCategories")]
        public async Task<IActionResult> EditCategories()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            CategoryListWithChosenCategory model = new CategoryListWithChosenCategory
            {
                Categories = categories.ToList(),
                IsRequired = false
            };
            return View(model);
        }

        [HttpPost("EditCategory")]
        public async Task<IActionResult> EditCategory(int categoryId)
        {
            var category = await _categoryService.GetCategoryAsync(categoryId);
            return PartialView("_EditCategory", category);
        }

        [HttpPut("EditCategory")]
        public async Task<IActionResult> EditCategory(int categoryId, string newName)
        {
            var category = new Category() { Name = newName };
            await _categoryService.UpdateCategoryAsync(categoryId, category);
            var categories = await _categoryService.GetCategoriesAsync();
            CategoryListWithChosenCategory model = new CategoryListWithChosenCategory
            {
                Categories = categories.ToList(),
                IsRequired = false
            };
            return PartialView("_EditCategoryList", model);
        }

        [HttpPost("EditProperty")]
        public async Task<IActionResult> EditProperty(int propertyId)
        {
            var property = await _propertyService.GetProperty(propertyId);
            return PartialView("_EditProperty", property);
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(string newCategoryName, int? parentId)
        {
            var newCategory = new Category() { Name = newCategoryName, ParentCategoryId = parentId };
            await _categoryService.AddCategoryAsync(newCategory);
            var categories = await _categoryService.GetCategoriesAsync();
            CategoryListWithChosenCategory model = new CategoryListWithChosenCategory
            {
                Categories = categories.ToList(),
                IsRequired = false
            };
            return PartialView("_EditCategoryList", model);
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            var categories = await _categoryService.GetCategoriesAsync();
            CategoryListWithChosenCategory model = new CategoryListWithChosenCategory
            {
                Categories = categories.ToList(),
                IsRequired = false
            };
            return PartialView("_EditCategoryList", model);
        }

        [HttpDelete("DeleteCategoryProperty")]
        public async Task<IActionResult> DeleteCategoryProperty(int categoryId, int propertyId)
        {
            await _categoryService.DeleteCategoryProperty(categoryId, propertyId);
            var category = await _categoryService.GetCategoryAsync(categoryId);
            return PartialView("_EditCategory", category);
        }

        [HttpPost("AddCategoryProperty")]
        public async Task<IActionResult> AddCategoryProperty(int categoryId, string propertyName)
        {
            Property property = new Property();
            property.Name = propertyName;
            await _categoryService.AddCategoryPropertyAsync(categoryId, property);
            var category = await _categoryService.GetCategoryAsync(categoryId);
            return PartialView("_EditCategory", category);
        }

        [HttpPost("AddPropertyValue")]
        public async Task<IActionResult> AddPropertyValue(int propertyId, string valueName)
        {
            PropertyValue propertyValue = new PropertyValue { Value = valueName };
            propertyValue = await _propertyService.AddPropertyValueAsync(propertyId, propertyValue);
            return PartialView("_PropertyValueRow", propertyValue);
        }

        [HttpPut("UpdateProperty")]
        public async Task UpdateProperty(Property property)
        {
            if (!ModelState.IsValid)
            {
                return;
            }
            await _propertyService.UpdatePropertyAsync(property.Id, property);
        }
    }
}