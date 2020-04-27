using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketX.BLL.DTOs
{
    public class Category
    {
        public Category()
        {
            ChildCategories = new List<Category>();
            Properties = new List<Property>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public List<Category> ChildCategories { get; set; }
        public List<Property> Properties { get; set; }
    }
}
