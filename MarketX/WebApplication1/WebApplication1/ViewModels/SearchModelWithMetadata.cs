using MarketX.BLL.DTOs;
using MarketX.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.ViewModels
{
    public class SearchModelWithMetadata
    {
        public SearchModelWithMetadata()
        {
            MainCategories = new List<Category>();
            Properties = new List<Property>();
            Counties = new List<County>();
            PropertyInputs = new List<PropertyWithValue>();
        }
        public SearchModelWithMetadata(SearchModel searchModel)
        {
            Name = searchModel.Name;
            CategoryId = searchModel.CategoryId;
            IsBuying = searchModel.IsBuying;
            FromPrice = searchModel.FromPrice;
            ToPrice = searchModel.ToPrice;
            PropertyInputs = new List<PropertyWithValue>();
            CountyId = searchModel.CountyId;
            CityId = searchModel.CityId;
            IsNew = searchModel.IsNew;
            IsUsed = searchModel.IsUsed;
            UserName = searchModel.UserName;
            IsWithPhoto = searchModel.IsWithPhoto;
            SortOrder = searchModel.SortOrder;

            MainCategories = new List<Category>();
            Properties = new List<Property>();
            Counties = new List<County>();
        }
        public string? Name { get; set; }
        public int? CategoryId { get; set; }
        public bool? IsBuying { get; set; }
        public int? FromPrice { get; set; }
        public int? ToPrice { get; set; }
        public List<PropertyWithValue> PropertyInputs { get; set; }
        public int? CountyId { get; set; }
        public int? CityId { get; set; }
        public bool IsNew { get; set; } = true;
        public bool IsUsed { get; set; } = true;
        public string? UserName { get; set; }
        public bool IsWithPhoto { get; set; }
        public BLL.Services.SortOrder? SortOrder { get; set; }

        public bool IsDetailed { get; set; }
        public List<Category> MainCategories { get; set; }
        public List<Property> Properties { get; set; }
        public List<County> Counties { get; set; }
    }
}