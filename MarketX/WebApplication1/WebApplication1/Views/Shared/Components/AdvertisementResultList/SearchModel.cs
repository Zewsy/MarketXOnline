using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Views.Shared.Components.AdvertisementResultList
{
    public class SearchModel
    {
        public string Name { get; set; }
        public int PriceFrom { get; set; }
        public int PriceTo { get; set; }
        public string CategoryName { get; set; }
        public string CountyName { get; set; }
        public string CityName { get; set; }
        public bool isNew { get; set; }

        public string SellerName { get; set; }

    }
}
