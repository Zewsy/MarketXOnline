using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketX.BLL.Interfaces;
using MarketX.ViewModels;
using MarketX.BLL.DTOs;
using MarketX.BLL.Utils;

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
        private readonly IAdvertisementService _advertisementService;
        public AdvertisementListViewComponent(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        public async Task<IViewComponentResult> InvokeAsync(AdvertisementType advertisementTypeToShow, int numberOfAdvertisementsToShow, BLL.DTOs.Advertisement? similarAdvertisement)
        {
            var advertisements = new List<BasicAdvertisementCard>();
            SearchModel searchModel = new SearchModel() { SortOrder = BLL.Services.SortOrder.Latest };
            switch (advertisementTypeToShow)
            {
                case AdvertisementType.Priorized:
                    searchModel.IsPriorized = true;
                    searchModel.IsWithPhoto = true;
                    break;
                case AdvertisementType.Latest:
                    searchModel.IsPriorized = false;
                    searchModel.IsWithPhoto = true;
                    searchModel.SortOrder = BLL.Services.SortOrder.Latest;
                    break;
                case AdvertisementType.Similar:
                    if (similarAdvertisement == null) throw new NullReferenceException("Nincs megadva hasonló hirdetés");
                    searchModel.CategoryId = similarAdvertisement.CategoryId;
                    if (similarAdvertisement.Seller == null)
                        searchModel.IsBuying = true;
                    else
                        searchModel.IsBuying = false;
                    break;
            }
            var dtoAdvertisements = await _advertisementService.GetAdvertisementsAsync(searchModel, numberOfAdvertisementsToShow);

            if (advertisementTypeToShow == AdvertisementType.Similar)
                dtoAdvertisements = FilterAds(numberOfAdvertisementsToShow, dtoAdvertisements, similarAdvertisement!);

            advertisements = dtoAdvertisements
                                        .Select(a => new BasicAdvertisementCard
                                        {
                                            Id = a.Id,
                                            Title = a.Title,
                                            Price = a.Price,
                                            ImagePath = a.AdvertisementImagePaths.FirstOrDefault(),
                                            City = a.City.Name,
                                            AdType = a.Seller == null ? AdType.Buying : AdType.Selling
                                        })
                                        .ToList();
            return View("AdvertisementList",advertisements);
        }

        private List<BLL.DTOs.Advertisement> FilterAds(int numberToShow, IEnumerable<BLL.DTOs.Advertisement> ads, BLL.DTOs.Advertisement similarAd)
        {
            IEnumerable<FilterType> filterOptions = new List<FilterType>()
            {
                FilterType.Price, FilterType.County, FilterType.City, FilterType.Condition, FilterType.ImportantProps, FilterType.OtherProps, FilterType.User
            };

            ads = ads.Where(a => a.Id != similarAd.Id);

            int i = 0;
            while(i < filterOptions.Count() && ads.Count() > numberToShow)
            {
                ads = FilterAdsByOption(ads, similarAd, filterOptions.ElementAt(i));
                i++;
            }
            return ads.Take(numberToShow).ToList();
        }

        private IEnumerable<BLL.DTOs.Advertisement> FilterAdsByOption(IEnumerable<BLL.DTOs.Advertisement> ads, BLL.DTOs.Advertisement similarAd, FilterType option)
        {
            switch (option)
            {
                case FilterType.Price:
                    return ads.Where(a => a.Price >= similarAd.Price / 2 && a.Price <= similarAd.Price * 2);
                case FilterType.County:
                    return ads.Where(a => a.City.County!.Name == similarAd.City.County!.Name);
                case FilterType.City:
                    return ads.Where(a => a.City.Name == similarAd.City.Name);
                case FilterType.Condition:
                    return ads.Where(a => a.Condition == similarAd.Condition);
                case FilterType.ImportantProps:
                    var importantProps = similarAd.AdvertisementProperties.Where(ap => ap.Property.IsImportant);
                    return AdvertisementFinder.FilterAdvertisementsByProperties(ads, importantProps);
                case FilterType.OtherProps:
                    var otherProps = similarAd.AdvertisementProperties.Where(ap => !ap.Property.IsImportant);
                    return AdvertisementFinder.FilterAdvertisementsByProperties(ads, otherProps);
                case FilterType.User:
                    return ads.Where(a => similarAd.Seller == null ? similarAd.Customer == a.Customer : similarAd.Seller == a.Seller);
                default:
                    return ads;
            }
        }
    }
}
