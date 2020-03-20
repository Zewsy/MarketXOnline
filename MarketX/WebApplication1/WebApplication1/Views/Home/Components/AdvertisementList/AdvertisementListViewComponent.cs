using MarketX.Data;
using MarketX.Models;
using MarketX.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MarketX.ViewModels.BasicAdvertisementCardViewModel;

namespace MarketX.Views.Home.ViewComponents
{
    public enum AdvertisementType
    {
        Priorized,
        Latest
    }
    public class AdvertisementListViewComponent : ViewComponent
    {
        private readonly MarketXContext context;
        public AdvertisementListViewComponent(MarketXContext _context)
        {
            context = _context;
        }

        public async Task<IViewComponentResult> InvokeAsync(AdvertisementType advertisementTypeToShow, int numberOfAdvertisementsToShow)
        {
            var advertisements = new List<BasicAdvertisementCardViewModel>();
            switch (advertisementTypeToShow)
            {
                case AdvertisementType.Priorized:
                    advertisements = await context.Advertisements.Where(a => a.IsPriorized && a.Status != Status.Closed)
                                                                 .Take(numberOfAdvertisementsToShow)
                                                                 .Select(a => new BasicAdvertisementCardViewModel
                                                                 { 
                                                                     Title = a.Title,
                                                                     Price = a.Price,
                                                                     ImagePath = a.AdvertisementPhotos.First().ImagePath,
                                                                     City = a.City.Name,
                                                                     AdType = a.Seller == null ? AdType.Buying : AdType.Selling
                                                                 })
                                                                 .ToListAsync();
                    break;
                case AdvertisementType.Latest:
                    advertisements = await context.Advertisements.Where(a => !a.IsPriorized && a.Status != Status.Closed)
                                                                 .OrderByDescending(a => a.CreatedDate)
                                                                 .Take(numberOfAdvertisementsToShow)
                                                                 .Select(a => new BasicAdvertisementCardViewModel
                                                                 {
                                                                     Title = a.Title,
                                                                     Price = a.Price,
                                                                     ImagePath = a.AdvertisementPhotos.First().ImagePath,
                                                                     City = a.City.Name,
                                                                     AdType = a.Seller == null ? AdType.Buying : AdType.Selling
                                                                 })
                                                                 .ToListAsync();
                    break;
            }
            return View("AdvertisementList",advertisements);
        }
    }
}
