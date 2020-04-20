using MarketX.Controllers;
using MarketX.Data;
using MarketX.Models;
using MarketX.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Views.Home.ViewComponents
{
    public enum AdvertisementType
    {
        Priorized,
        Latest,
        Similar
    }

    public enum FilterType
    {
        Price,
        County,
        City,
        Condition,
        ImportantProps,
        OtherProps,
        User
    }

    public class AdvertisementListViewComponent : ViewComponent
    {
        private readonly MarketXContext context;
        public AdvertisementListViewComponent(MarketXContext _context)
        {
            context = _context;
        }

        public async Task<IViewComponentResult> InvokeAsync(AdvertisementType advertisementTypeToShow, int numberOfAdvertisementsToShow, Models.Advertisement? similarAdvertisement)
        {
            var advertisements = new List<BasicAdvertisementCard>();
            switch (advertisementTypeToShow)
            {
                case AdvertisementType.Priorized:
                    advertisements = await context.Advertisements.Where(a => a.IsPriorized && a.Status == Status.Active)
                                                                 .Take(numberOfAdvertisementsToShow)
                                                                 .Select(a => new BasicAdvertisementCard
                                                                 { 
                                                                     ID = a.ID,
                                                                     Title = a.Title,
                                                                     Price = a.Price,
                                                                     ImagePath = a.AdvertisementPhotos.First().ImagePath,
                                                                     City = a.City.Name,
                                                                     AdType = a.Seller == null ? AdType.Buying : AdType.Selling
                                                                 })
                                                                 .ToListAsync();
                    break;
                case AdvertisementType.Latest:
                    advertisements = await context.Advertisements.Where(a => !a.IsPriorized && a.Status == Status.Active && a.AdvertisementPhotos.Any())
                                                                 .OrderByDescending(a => a.CreatedDate)
                                                                 .Take(numberOfAdvertisementsToShow)
                                                                 .Select(a => new BasicAdvertisementCard
                                                                 {
                                                                     ID = a.ID,
                                                                     Title = a.Title,
                                                                     Price = a.Price,
                                                                     ImagePath = a.AdvertisementPhotos.First().ImagePath,
                                                                     City = a.City.Name,
                                                                     AdType = a.Seller == null ? AdType.Buying : AdType.Selling
                                                                 })
                                                                 .ToListAsync();
                    break;
                case AdvertisementType.Similar:
                    //TODO: Similarities
                    if (similarAdvertisement == null) throw new NullReferenceException("Nincs megadva hasonló hirdetés");
                    var dbCategories = context.Categories.ToList();
                    var properCategoryNames = Category.GetProperCategoryNamesFor(similarAdvertisement.Category.Name, dbCategories);
                    var dbAdvertisements = await context.Advertisements
                                                                 .Include(a => a.City)
                                                                    .ThenInclude(c => c.County)
                                                                 .Include(a => a.Category)
                                                                 .Include(a => a.AdvertisementPhotos)
                                                                 .Include(a => a.Seller)
                                                                 .Include(a => a.Customer)
                                                                 .Where(a => properCategoryNames.Contains(a.Category.Name)
                                                                                                && a.ID != similarAdvertisement.ID
                                                                                                && a.Status == Status.Active
                                                                                                && (similarAdvertisement.Seller == null ? a.Seller == null : a.Customer == null)
                                                                 )
                                                                 .ToListAsync();
                    advertisements = FilterAds(numberOfAdvertisementsToShow, dbAdvertisements, similarAdvertisement)
                                        .Select(a => new BasicAdvertisementCard
                                        {
                                            ID = a.ID,
                                            Title = a.Title,
                                            Price = a.Price,
                                            ImagePath = a.AdvertisementPhotos.FirstOrDefault()?.ImagePath,
                                            City = a.City.Name,
                                            AdType = a.Seller == null ? AdType.Buying : AdType.Selling
                                        }).ToList();
                    break;
            }
            return View("AdvertisementList",advertisements);
        }

        private IEnumerable<Models.Advertisement> FilterAds(int numberToShow, IEnumerable<Models.Advertisement> ads, Models.Advertisement similarAd)
        {
            IEnumerable<FilterType> filterOptions = new List<FilterType>()
            {
                FilterType.Price, FilterType.County, FilterType.City, FilterType.Condition, FilterType.ImportantProps, FilterType.OtherProps, FilterType.User
            };

            int i = 0;
            while(i < filterOptions.Count() && ads.Count() > numberToShow)
            {
                ads = FilterAdsByOption(ads, similarAd, filterOptions.ElementAt(i));
                i++;
            }
            return ads.Take(numberToShow);
        }

        private IEnumerable<Models.Advertisement> FilterAdsByOption(IEnumerable<Models.Advertisement> ads, Models.Advertisement similarAd, FilterType option)
        {
            switch (option)
            {
                case FilterType.Price:
                    return ads.Where(a => a.Price >= similarAd.Price / 2 && a.Price <= similarAd.Price * 2);
                case FilterType.County:
                    return ads.Where(a => a.City.County.Name == similarAd.City.County.Name);
                case FilterType.City:
                    return ads.Where(a => a.City.Name == similarAd.City.Name);
                case FilterType.Condition:
                    return ads.Where(a => a.Condition == similarAd.Condition);
                case FilterType.ImportantProps:
                    var importantProps = similarAd.AdvertisementProperties.Where(ap => ap.Property.isImportant);
                    return Models.Advertisement.FilterAdvertisementsByProperties(ads, AdvertisementProperty.ConvertToPropertyInputFieldList(importantProps));
                case FilterType.OtherProps:
                    var otherProps = similarAd.AdvertisementProperties.Where(ap => !ap.Property.isImportant);
                    return Models.Advertisement.FilterAdvertisementsByProperties(ads, AdvertisementProperty.ConvertToPropertyInputFieldList(otherProps));
                case FilterType.User:
                    return ads.Where(a => similarAd.Seller == null ? similarAd.Customer == a.Customer : similarAd.Seller == a.Seller);
                default:
                    return ads;
            }
        }
    }
}
