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
        public Category? ParentCategory { get; set; }
        public ICollection<CategoryProperty> CategoryProperties { get; set; }
    }
}
