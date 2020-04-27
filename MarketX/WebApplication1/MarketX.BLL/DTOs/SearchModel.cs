using MarketX.BLL.Services;
using System.Collections.Generic;

namespace MarketX.BLL.DTOs
{
    public class SearchModel
    {
        public SearchModel()
        {
            PropertyInputs = new List<PropertyWithValue>();
            IsNew = true;
            IsUsed = true;
            IsWithPhoto = false;
        }

        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsBuying { get; set; }
        public int? FromPrice { get; set; }
        public int? ToPrice { get; set; }
        public List<PropertyWithValue> PropertyInputs { get; set; }
        public int? CountyId { get; set; }
        public int? CityId { get; set; }
        public bool IsNew { get; set; }
        public bool IsUsed { get; set; }
        public string? UserName { get; set; }
        public bool IsWithPhoto { get; set; }
        public bool? IsPriorized { get; set; }
        public SortOrder? SortOrder { get; set; }
    }
}
