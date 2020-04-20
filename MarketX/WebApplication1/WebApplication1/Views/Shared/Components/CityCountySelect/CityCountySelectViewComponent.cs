using MarketX.Data;
using MarketX.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MarketX.Views.Shared.Components.CityCountySelect
{
    public class CityCountySelectViewComponent : ViewComponent
    {
        private readonly MarketXContext context;
        public CityCountySelectViewComponent(MarketXContext _context)
        {
            context = _context;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool isRequired = false)
        {
            var counties = await context.Counties.ToListAsync();
            MarketX.ViewModels.CityCountySelect model = new MarketX.ViewModels.CityCountySelect(counties) { IsRequired = isRequired };

            return View("CityCountySelect", model);
        }
    }
}
