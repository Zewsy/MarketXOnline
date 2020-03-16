using MarketX.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.ViewModels
{
    public enum AdType
    {
        Selling,
        Buying
    }
    public class BasicAdvertisementCardViewModel
    {

        public string Title { get; set; }
        public int? Price { get; set; }
        public string ImagePath { get; set; }
        public string City { get; set; }
        public AdType AdType { get; set; }
    }
}
