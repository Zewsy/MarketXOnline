using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MarketX.BLL.DTOs;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using MarketX.BLL.Interfaces;
using MarketX.ViewModels;

namespace MarketX.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICityCountyService _cityCountyService;
        private readonly IAdvertisementService _advertisementService;
        public HomeController(ICategoryService categoryService, ICityCountyService cityCountyService, IAdvertisementService advertisementService)
        {
            _categoryService = categoryService;
            _cityCountyService = cityCountyService;
            _advertisementService = advertisementService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ChooseCategory(int categoryId)
        {
            var props = await _categoryService.GetCategoryPropertiesAsync(categoryId);
            List<PropertyWithValue> inputFields = await props.Select(p => new PropertyWithValue(p)).ToListAsync();
            PropertyInputList inputs = new PropertyInputList { PropertyInputs = inputFields };
            return PartialView("_Properties", inputs);
        }

        public async Task<IActionResult> ChooseCounty(int countyId)
        {
            var cities = await _cityCountyService.GetCitiesInCountyAsync(countyId);
            return PartialView("/Views/Shared/Components/CityCountySelect/_CitySelect.cshtml", cities);
        }

        public async Task<IActionResult> Results(SearchModel searchModel, int? page)
        {
            ResultsWithSearchModel model;
            if (!ModelState.IsValid)
            {
                model = new ResultsWithSearchModel(searchModel, new PagedList<ResultAdvertisementCard>(new List<ResultAdvertisementCard>(),1,5));
                return View(model);
            }

            var advertisements = await _advertisementService.GetAdvertisementsAsync(searchModel);

            List<ResultAdvertisementCard> results = ParseAdvertisements(advertisements);

            int pageNumber = (page ?? 1);
            int pageSize = 5;
            model = new ResultsWithSearchModel(searchModel, results.ToPagedList(pageNumber, pageSize));

            return View(model);
        }

        private List<ResultAdvertisementCard> ParseAdvertisements(List<Advertisement> advertisements)
        {
            return advertisements.Select(a => new ResultAdvertisementCard
            {
                ID = a.Id,
                AdType = a.Seller == null ? AdType.Buying : AdType.Selling,
                County = a.City.County?.Name,
                City = a.City.Name,
                Condition = a.Condition,
                ImagePath = a.AdvertisementImagePaths.Any() ? a.AdvertisementImagePaths.First() : Url.Content("~/images/image-placeholder.jpg"),
                IsPriorized = a.IsPriorized,
                Price = a.Price,
                Title = a.Title,
                UserName = a.Seller == null ? a.Customer!.LastName + " " + a.Customer.FirstName : a.Seller.LastName + " " + a.Seller.FirstName
            })
            .ToList();
        }
    }
}
