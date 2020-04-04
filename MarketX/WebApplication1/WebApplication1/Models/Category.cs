using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryID { get; set; }
        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<CategoryProperty> CategoryProperties { get; set; }
        public virtual ICollection<Category> ChildCategories { get; set; }
    }
}
