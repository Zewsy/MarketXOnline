using MarketX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.ViewModels
{
    public class CityCountySelect
    {
        public CityCountySelect(ICollection<County> counties)
        {
            Counties = counties;
            IsRequired = false;
        }
        public ICollection<County> Counties { get; set; }
        public string? County { get; set; }
        public string? City { get; set; }
        public bool IsRequired { get; set; }

    }
}
