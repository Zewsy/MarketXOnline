using MarketX.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarketX.BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryAsync(int categoryId);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<IEnumerable<Category>> GetMainCategoriesAsync();
        Task<IEnumerable<Property>> GetCategoryPropertiesAsync(int categoryId);
        Task<IEnumerable<Category>> GetAllProperCategoriesForAsync(int categoryId);
        Task<Category> AddCategoryAsync(Category category);
        Task DeleteCategoryAsync(int categoryId);
        Task UpdateCategoryAsync(int categoryId, Category category);
        Task DeleteCategoryProperty(int categoryId, int propertyId);
        Task<Property> AddCategoryPropertyAsync(int categoryId, Property property);
    }
}
