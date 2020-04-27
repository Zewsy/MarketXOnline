using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.DAL.Entities
{
    public class Category
    {
        public Category(string name)
        {
            Name = name;
            CategoryProperties = new List<CategoryProperty>();
            ChildCategories = new List<Category>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; } = null!;
        public virtual ICollection<CategoryProperty> CategoryProperties { get; set; }
        public virtual ICollection<Category> ChildCategories { get; set; }
    }
}
