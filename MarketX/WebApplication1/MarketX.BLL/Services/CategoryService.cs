using AutoMapper;
using MarketX.BLL.DTOs;
using MarketX.BLL.Interfaces;
using MarketX.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketX.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly MarketXContext _context;
        private readonly IMapper _mapper;

        public CategoryService(MarketXContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var dbCategories = await _context.Categories.ToListAsync();
            return _mapper.Map<List<Category>>(dbCategories);
        }

        public async Task<IEnumerable<Property>> GetCategoryPropertiesAsync(int categoryId)
        {
            var dbCategoryProperties = await _context.CategoryProperties
                                                  .Include(cp => cp.Property)
                                                    .ThenInclude(p => p.PropertyValues)
                                                  .Where(cp => cp.Category.Id == categoryId)
                                                  .Select(cp => cp.Property)
                                                  .OrderBy(p => p.ValueType).ToListAsync();
            return _mapper.Map<List<Property>>(dbCategoryProperties);
        }

        public async Task<IEnumerable<Category>> GetAllProperCategoriesForAsync(int categoryId)
        {
            var categories = await GetCategoriesAsync();
            List<Category> childCategories = new List<Category>();
            var category = categories.FirstOrDefault(c => c.Id == categoryId);
            AddAllChildrenCategoryNamesToList(childCategories, category);
            return childCategories;
        }

        private void AddAllChildrenCategoryNamesToList(List<Category> categories, Category parentCategory)
        {
            categories.Add(parentCategory);
            if (parentCategory.ChildCategories.Any())
            {
                foreach (var category in parentCategory.ChildCategories)
                {
                    AddAllChildrenCategoryNamesToList(categories, category);
                }
            }
        }

        public async Task<IEnumerable<Category>> GetMainCategoriesAsync()
        {
            var dbCategories = await _context.Categories.Where(c => c.ParentCategoryId == null).ToListAsync();
            return _mapper.Map<List<Category>>(dbCategories);
        }
    }
}
