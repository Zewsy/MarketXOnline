using MarketX.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarketX.BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<IEnumerable<Category>> GetMainCategoriesAsync();
        Task<IEnumerable<Property>> GetCategoryPropertiesAsync(int categoryId);
        Task<IEnumerable<Category>> GetAllProperCategoriesForAsync(int categoryId);
    }
}
