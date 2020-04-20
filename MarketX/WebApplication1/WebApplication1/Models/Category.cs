using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Models
{
    public class Category
    {
        public Category(string name)
        {
            Name = name;
            CategoryProperties = new List<CategoryProperty>();
            ChildCategories = new List<Category>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryID { get; set; }
        public virtual Category ParentCategory { get; set; } = null!;
        public virtual ICollection<CategoryProperty> CategoryProperties { get; set; }
        public virtual ICollection<Category> ChildCategories { get; set; }

        public static ICollection<string> GetProperCategoryNamesFor(string categoryName, List<Category> dbCategories)
        {
            List<string> categories = new List<string>();
            var dbCategory = dbCategories.Where(c => c.Name == categoryName).FirstOrDefault();
            AddAllChildrenCategoryNamesToList(categories, dbCategory);
            return categories;
        }

        private static void AddAllChildrenCategoryNamesToList(List<string> categoryNames, Category parentCategory)
        {
            categoryNames.Add(parentCategory.Name);
            if (parentCategory.ChildCategories.Any())
            {
                foreach (var category in parentCategory.ChildCategories)
                {
                    AddAllChildrenCategoryNamesToList(categoryNames, category);
                }
            }
        }
    }
}
