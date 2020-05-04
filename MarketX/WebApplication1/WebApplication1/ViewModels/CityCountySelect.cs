
using MarketX.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.ViewModels
{
    public class CityCountySelect
    {
        public CityCountySelect(List<County> counties)
        {
            Counties = counties;
            Cities = new List<City>();
            IsRequired = false;
        }
        public List<County> Counties { get; set; }
        public List<City> Cities { get; set; }
        public int? CountyId { get; set; }
        public int? CityId { get; set; }
        public bool IsRequired { get; set; }

    }
}
