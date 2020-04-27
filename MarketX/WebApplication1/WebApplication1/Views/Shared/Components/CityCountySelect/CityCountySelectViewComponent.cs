using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using MarketX.BLL.Interfaces;
using System.Linq;

namespace MarketX.Views.Shared.Components.CityCountySelect
{
    public class CityCountySelectViewComponent : ViewComponent
    {
        private readonly ICityCountyService _cityCountyService;
        public CityCountySelectViewComponent(ICityCountyService cityCountyService)
        {
            _cityCountyService = cityCountyService;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool isRequired = false)
        {
            var counties = await _cityCountyService.GetCountiesAsync();
            ViewModels.CityCountySelect model = new ViewModels.CityCountySelect(counties.ToList()) { IsRequired = isRequired };

            return View("CityCountySelect", model);
        }
    }
}
