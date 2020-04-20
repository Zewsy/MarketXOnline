using MarketX.Controllers;
using MarketX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.ViewModels
{
    public class SearchFormModelWithMetadata
    {
        public SearchFormModelWithMetadata()
        {
            MainCategories = new List<Category>();
            PropertyInputs = new List<PropertyInputField>();
            Properties = new List<Property>();
            Counties = new List<County>();
        }
        public SearchFormModelWithMetadata(SearchFormModel searchFormModel)
        {
            Name = searchFormModel.Name;
            Category = searchFormModel.Category;
            IsBuying = searchFormModel.IsBuying;
            FromPrice = searchFormModel.FromPrice;
            ToPrice = searchFormModel.ToPrice;
            PropertyInputs = searchFormModel.PropertyInputs;
            County = searchFormModel.County;
            City = searchFormModel.City;
            IsNew = searchFormModel.IsNew;
            IsUsed = searchFormModel.IsUsed;
            UserName = searchFormModel.UserName;
            IsWithPhoto = searchFormModel.IsWithPhoto;
            SortOrder = searchFormModel.SortOrder;

            MainCategories = new List<Category>();
            PropertyInputs = new List<PropertyInputField>();
            Properties = new List<Property>();
            Counties = new List<County>();
        }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public bool? IsBuying { get; set; }
        public int? FromPrice { get; set; }
        public int? ToPrice { get; set; }
        public List<PropertyInputField> PropertyInputs { get; set; }
        public string? County { get; set; }
        public string? City { get; set; }
        public bool IsNew { get; set; } = true;
        public bool IsUsed { get; set; } = true;
        public string? UserName { get; set; }
        public bool IsWithPhoto { get; set; }
        public SortOrder? SortOrder { get; set; }



        public bool IsDetailed { get; set; }
        public List<Category> MainCategories { get; set; }
        public List<Property> Properties { get; set; }
        public List<County> Counties { get; set; }
    }
}