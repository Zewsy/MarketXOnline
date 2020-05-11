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

        public async Task<Category> GetCategoryAsync(int categoryId)
        {
            var dbCategory = await _context.Categories
                                    .Include(c => c.CategoryProperties)
                                        .ThenInclude(cp => cp.Property)
                                    .FirstOrDefaultAsync(c => c.Id == categoryId);
            return _mapper.Map<Category>(dbCategory);
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            var dbCategory = _mapper.Map<DAL.Entities.Category>(category);
            _context.Categories.Add(dbCategory);
            await _context.SaveChangesAsync();
            return _mapper.Map<Category>(dbCategory);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var categories = await _context.Categories.ToListAsync();
            DAL.Entities.Category category = categories.First(c => c.Id == categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                foreach(var child in category.ChildCategories)
                    DeleteChildCategory(child);
                await _context.SaveChangesAsync();
            }
        }

        private void DeleteChildCategory(DAL.Entities.Category category)
        {
            _context.Categories.Remove(category);
            foreach (var childCategory in category.ChildCategories)
            {
                DeleteChildCategory(childCategory);
            }
        }

        public async Task UpdateCategoryAsync(int categoryId, Category category)
        {
            if (category.Name == null)
                return;

            var dbCategory = _context.Categories.First(c => c.Id == categoryId);
            dbCategory.Name = category.Name;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryProperty(int categoryId, int propertyId)
        {
            var dbCategoryProperty = await _context.CategoryProperties.FirstAsync(cp => cp.CategoryId == categoryId && cp.PropertyId == propertyId);
            _context.CategoryProperties.Remove(dbCategoryProperty);
            await _context.SaveChangesAsync();
        }

        public async Task<Property> AddCategoryPropertyAsync(int categoryId, Property property)
        {
            var dbCategory = await _context.Categories.FirstAsync(c => c.Id == categoryId);
            var dbProperty = await _context.Properties.FirstOrDefaultAsync(p => p.Name == property.Name);
            dbProperty = dbProperty ?? _mapper.Map<DAL.Entities.Property>(property);
            dbCategory.CategoryProperties.Add(new DAL.Entities.CategoryProperty { CategoryId = categoryId, Property = dbProperty });
            await _context.SaveChangesAsync();
            return _mapper.Map<Property>(dbProperty);
        }
    }
}
