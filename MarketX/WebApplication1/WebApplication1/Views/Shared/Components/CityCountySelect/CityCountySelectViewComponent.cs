using MarketX.Data;
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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var counties = await context.Counties.ToListAsync();

            return View("CityCountySelect", counties);
        }
    }
}
