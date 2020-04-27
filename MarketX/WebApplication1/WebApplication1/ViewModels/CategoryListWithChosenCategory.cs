using MarketX.BLL.DTOs;
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
        public List<Category> Categories { get; set; }
        public int? ChosenCategoryId  { get; set; }
        public bool IsRequired { get; set; }
    }
}
