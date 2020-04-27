

using MarketX.BLL.DTOs;

namespace MarketX.ViewModels
{
    public class CategoryWithChosenCategory
    {
        public Category Category { get; set; } = null!;
        public int? ChosenCategoryId { get; set; }
        public bool IsRequired { get; set; }
    }
}
