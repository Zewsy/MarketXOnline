using MarketX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.ViewModels
{
    public class CategoryListWithChosenCategory
    {
        public CategoryListWithChosenCategory()
        {
            IsRequired = false;
            Categories = new List<Category>();
        }
        public ICollection<Category> Categories { get; set; }
        public string? ChosenCategoryName  { get; set; }
        public bool IsRequired { get; set; }
    }
}
