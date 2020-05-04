using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using MarketX.BLL.Interfaces;
using System.Linq;
using System.Collections.Generic;
using MarketX.BLL.DTOs;

namespace MarketX.Views.Shared.Components.CityCountySelect
{
    public class CityCountySelectViewComponent : ViewComponent
    {
        private readonly ICityCountyService _cityCountyService;
        public CityCountySelectViewComponent(ICityCountyService cityCountyService)
        {
            _cityCountyService = cityCountyService;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool isRequired = false, int? cityId = null)
        {
            var counties = await _cityCountyService.GetCountiesAsync();
            IEnumerable<City> cities = new List<City>();
            int? countyId = null;
            if(cityId != null)
            {
                var city = await _cityCountyService.GetCity((int)cityId);
                countyId = city.County!.Id;
                cities = await _cityCountyService.GetCitiesInCountyAsync((int)countyId);
            }
            ViewModels.CityCountySelect model = new ViewModels.CityCountySelect(counties.ToList()) { IsRequired = isRequired, CityId = cityId, CountyId = countyId, Cities = cities.ToList() };

            return View("CityCountySelect", model);
        }
    }
}
