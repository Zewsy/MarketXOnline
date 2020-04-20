using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MarketX.Models;
using MarketX.Data;
using MarketX.ViewModels;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace MarketX.Controllers
{
    public enum SortOrder
    {
        DescendingByPrice,
        AscendingByPrice,
        Latest,
        AscendingByABC
    }

    public class HomeController : Controller
    {
        private readonly MarketXContext context;

        public HomeController(MarketXContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChooseCategory(string CategoryName)
        {
            var props = context.CategoryProperties.Include(cp => cp.Property)
                                                    .ThenInclude(p => p.PropertyValues)
                                                  .Where(cp => cp.Category.Name == CategoryName)
                                                  .Select(cp => cp.Property)
                                                  .OrderBy(p => p.ValueType).ToList();
            PropertiesWithInputs model = new PropertiesWithInputs { Properties = props };
            return PartialView("_Properties", model);
        }

        public IActionResult ChooseCounty(string CountyName)
        {
            var cities = context.Counties.Where(c => c.Name == CountyName).Select(c => c.Cities).FirstOrDefault();
            return PartialView("/Views/Shared/Components/CityCountySelect/_CitySelect.cshtml", cities);
        }

        public async Task<IActionResult> Results(SearchFormModel searchFormModel, int? page)
        {
            var advertisements = GetProperAdvertisements(searchFormModel);
            advertisements = SortAdvertisements(advertisements, searchFormModel.SortOrder);

            var PropertyInputs = searchFormModel.PropertyInputs.Where(pi => pi.Value != null && pi.Value != "false").ToList();
            advertisements = await Advertisement.FilterAdvertisementsByProperties(advertisements, PropertyInputs).ToListAsync();

            List<ResultAdvertisementCard> results = ParseAdvertisements(advertisements);

            int pageNumber = (page ?? 1);
            int pageSize = 5;
            ResultsWithSearchFormModel model = new ResultsWithSearchFormModel(searchFormModel, results.ToPagedList(pageNumber, pageSize));

            return View(model);
        }

        private IEnumerable<Advertisement> GetProperAdvertisements(SearchFormModel searchFormModel)
        {
            var dbCategories = context.Categories.ToList();
            return context.Advertisements
                                    .Include(a => a.Seller)
                                    .Include(a => a.Customer)
                                    .Include(a => a.City)
                                        .ThenInclude(c => c.County)
                                    .Include(a => a.AdvertisementPhotos)
                                    .Include(a => a.AdvertisementProperties)
                                        .ThenInclude(ap => ap.Property)
                                    .Include(a => a.Category)
                                    .Where(a =>
                                    (a.Status == Status.Active) &&
                                    (searchFormModel.Name == null || a.Title.Contains(searchFormModel.Name)) &&
                                    (searchFormModel.Category == null || Category.GetProperCategoryNamesFor(searchFormModel.Category, dbCategories).Contains(a.Category.Name)) &&
                                    (searchFormModel.County == null || a.City.County.Name == searchFormModel.County) &&
                                    (searchFormModel.City == null || a.City.Name == searchFormModel.City) &&
                                    (searchFormModel.IsBuying == null || (searchFormModel.IsBuying == true && a.Seller == null) || (searchFormModel.IsBuying == false && a.Customer == null)) &&
                                    ((searchFormModel.IsNew && a.Condition == Condition.New) || (searchFormModel.IsUsed && a.Condition == Condition.Used)) &&
                                    (searchFormModel.IsWithPhoto == false || a.AdvertisementPhotos.Any()) &&
                                    (searchFormModel.UserName == null
                                     || (a.Seller != null && (a.Seller.FirstName.Contains(searchFormModel.UserName) || (a.Seller.LastName.Contains(searchFormModel.UserName))))
                                     || (a.Customer != null && (a.Customer.FirstName.Contains(searchFormModel.UserName) || (a.Customer.LastName.Contains(searchFormModel.UserName))))) &&
                                    (searchFormModel.FromPrice == null || a.Price >= searchFormModel.FromPrice) &&
                                    (searchFormModel.ToPrice == null || a.Price <= searchFormModel.ToPrice));
        }

        private IEnumerable<Advertisement> SortAdvertisements(IEnumerable<Advertisement> advertisements, SortOrder? order)
        {
            switch (order)
            {
                case SortOrder.DescendingByPrice:
                    return advertisements.OrderByDescending(a => a.IsPriorized)
                                    .ThenByDescending(a => a.Price);
                case SortOrder.AscendingByPrice:
                    return advertisements.OrderByDescending(a => a.IsPriorized)
                                    .ThenBy(a => a.Price);
                case SortOrder.Latest:
                    return advertisements.OrderByDescending(a => a.IsPriorized)
                                    .ThenByDescending(a => a.CreatedDate);
                case SortOrder.AscendingByABC:
                    return advertisements.OrderByDescending(a => a.IsPriorized)
                                    .ThenBy(a => a.Title);
                default:
                    return advertisements.OrderByDescending(a => a.IsPriorized)
                                    .ThenByDescending(a => a.Price);
            }
        }

        private List<ResultAdvertisementCard> ParseAdvertisements(IEnumerable<Advertisement> advertisements)
        {
            return advertisements.Select(a => new ResultAdvertisementCard
            {
                ID = a.ID,
                AdType = a.Seller == null ? AdType.Buying : AdType.Selling,
                County = a.City.County.Name,
                City = a.City.Name,
                Condition = a.Condition,
                ImagePath = a.AdvertisementPhotos.Any() ? a.AdvertisementPhotos.First().ImagePath : Url.Content("~/images/image-placeholder.jpg"),
                IsPriorized = a.IsPriorized,
                Price = a.Price,
                Title = a.Title,
                UserName = a.Seller == null ? a.Customer!.LastName + " " + a.Customer.FirstName : a.Seller.LastName + " " + a.Seller.FirstName
            })
            .ToList();
        }
    }
}
