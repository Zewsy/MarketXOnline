using MarketX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.ViewModels
{
    public class CategoryWithChosenCategory
    {
        public Category Category { get; set; } = null!;
        public string? ChosenCategoryName { get; set; }
        public bool IsRequired { get; set; }
    }
}
