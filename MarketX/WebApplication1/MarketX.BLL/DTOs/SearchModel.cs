using MarketX.BLL.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        [Range(0, int.MaxValue, ErrorMessage = "Negatív érték nem adható meg minimum árnak!")]
        public int? FromPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Currency)]
        [Range(0, int.MaxValue, ErrorMessage = "Negatív érték nem adható meg maximum árnak!")]
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
        
        [EmailAddress]
        public string? Email { get; set; }
    }
}
