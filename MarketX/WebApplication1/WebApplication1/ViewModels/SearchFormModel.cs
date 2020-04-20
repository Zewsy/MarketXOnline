using MarketX.Controllers;
using MarketX.Models;
using System.Collections.Generic;

namespace MarketX.ViewModels
{
    public class SearchFormModel
    {
        public SearchFormModel()
        {
            PropertyInputs = new List<PropertyInputField>();
            IsNew = true;
            IsUsed = true;
            IsWithPhoto = false;
        }

        public string? Name { get; set; }
        public string? Category { get; set; }
        public bool? IsBuying { get; set; }
        public int? FromPrice { get; set; }
        public int? ToPrice { get; set; }
        public List<PropertyInputField> PropertyInputs { get; set; }
        public string? County { get; set; }
        public string? City { get; set; }
        public bool IsNew { get; set; }
        public bool IsUsed { get; set; }
        public string? UserName { get; set; }
        public bool IsWithPhoto { get; set; }
        public SortOrder? SortOrder { get; set; }
    }
}
