using MarketX.BLL.DTOs;
using MarketX.BLL.Interfaces;
using MarketX.BLL.Services;
using MarketX.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketX.BLL.Utils
{
    public static class AdvertisementFinder
    {
        public static IQueryable<DAL.Entities.Advertisement> FilterAdvertisements(MarketXContext context, SearchModel searchModel)
        {
            var dbAdvertisements = context.Advertisements
                                    .Include(a => a.Seller)
                                    .Include(a => a.Customer)
                                    .Include(a => a.City)
                                        .ThenInclude(c => c.County)
                                    .Include(a => a.AdvertisementPhotos)
                                    .Include(a => a.AdvertisementProperties)
                                        .ThenInclude(ap => ap.Property)
                                    .Include(a => a.Category)
                                    .CheckConditions(searchModel);
            return SortAdvertisements(dbAdvertisements, searchModel.SortOrder);
        }

        private static IQueryable<DAL.Entities.Advertisement> CheckConditions(this IQueryable<DAL.Entities.Advertisement> advertisements, SearchModel searchModel)
        {
            return advertisements.CheckStatus()
                                 .CheckIsPriorized(searchModel.IsPriorized)
                                 .CheckName(searchModel.Name)
                                 .CheckCityCounty(searchModel.CountyId, searchModel.CityId)
                                 .CheckIsBuying(searchModel.IsBuying)
                                 .CheckCondition(searchModel.IsNew, searchModel.IsUsed)
                                 .CheckImages(searchModel.IsWithPhoto)
                                 .CheckUserName(searchModel.UserName)
                                 .CheckPrice(searchModel.FromPrice, searchModel.ToPrice)
                                 .CheckEmail(searchModel.Email);
        }

        private static IQueryable<DAL.Entities.Advertisement> CheckIsPriorized(this IQueryable<DAL.Entities.Advertisement> advertisements, bool? IsPriorized)
        {
            return advertisements.Where(a => IsPriorized == null || a.IsPriorized == IsPriorized);
        }

        private static IQueryable<DAL.Entities.Advertisement> CheckStatus(this IQueryable<DAL.Entities.Advertisement> advertisements)
        {
            return advertisements.Where(a => a.Status == DAL.Entities.Status.Active);
        }

        private static IQueryable<DAL.Entities.Advertisement> CheckName(this IQueryable<DAL.Entities.Advertisement> advertisements, string? Name)
        {
            return advertisements.Where(a => Name == null || a.Title.Contains(Name));
        }

        private static IQueryable<DAL.Entities.Advertisement> CheckCityCounty(this IQueryable<DAL.Entities.Advertisement> advertisements, int? CountyId, int? CityId)
        {
            return advertisements.Where(a => (CountyId == null || a.City.CountyId == CountyId) &&
                                       (CityId == null || a.City.Id == CityId));
        }

        private static IQueryable<DAL.Entities.Advertisement> CheckIsBuying(this IQueryable<DAL.Entities.Advertisement> advertisements, bool? IsBuying)
        {
            return advertisements.Where(a => IsBuying == null ||
                                       (IsBuying == true && a.Seller == null) ||
                                       (IsBuying == false && a.Customer == null));
        }

        private static IQueryable<DAL.Entities.Advertisement> CheckCondition(this IQueryable<DAL.Entities.Advertisement> advertisements, bool IsNew, bool IsUsed)
        {
            return advertisements.Where(a =>
                                       (IsNew && a.Condition == DAL.Entities.Condition.New) ||
                                       (IsUsed && a.Condition == DAL.Entities.Condition.Used));
        }

        private static IQueryable<DAL.Entities.Advertisement> CheckImages(this IQueryable<DAL.Entities.Advertisement> advertisements, bool IsWithPhoto)
        {
            return advertisements.Where(a => IsWithPhoto == false || a.AdvertisementPhotos.Any());
        }

        private static IQueryable<DAL.Entities.Advertisement> CheckUserName(this IQueryable<DAL.Entities.Advertisement> advertisements, string? UserName)
        {
            if(UserName != null && UserName.Split(" ").Length >= 2)
            {
                string[] names = UserName.Split(" ");
                string lastName = names[0];
                StringBuilder firstNameSb = new StringBuilder();
                for (int i = 1; i < names.Length; i++){
                    firstNameSb.Append(names[i]);
                }
                string firstName = firstNameSb.ToString();
                return advertisements.Where(a =>
                                       (a.Seller != null && (a.Seller.FirstName.Contains(firstName) && (a.Seller.LastName.Contains(lastName)))) ||
                                       (a.Customer != null && (a.Customer.FirstName.Contains(firstName) && (a.Customer.LastName.Contains(lastName)))));
            }
            return advertisements.Where(a => UserName == null ||
                                       (a.Seller != null && (a.Seller.FirstName.Contains(UserName) || (a.Seller.LastName.Contains(UserName)))) ||
                                       (a.Customer != null && (a.Customer.FirstName.Contains(UserName) || (a.Customer.LastName.Contains(UserName)))));
        }

        private static IQueryable<DAL.Entities.Advertisement> CheckPrice(this IQueryable<DAL.Entities.Advertisement> advertisements, int? FromPrice, int? ToPrice)
        {
            return advertisements.Where(a =>
                                       (FromPrice == null || a.Price >= FromPrice) &&
                                       (ToPrice == null || a.Price <= ToPrice));
        }

        private static IQueryable<DAL.Entities.Advertisement> CheckEmail(this IQueryable<DAL.Entities.Advertisement> advertisements, string? email)
        {
            return advertisements.Where(a => email == null || a.Seller.Email == email || a.Customer.Email == email);
        }

        private static IQueryable<DAL.Entities.Advertisement> SortAdvertisements(IQueryable<DAL.Entities.Advertisement> advertisements, SortOrder? order)
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

        public async static Task<List<Advertisement>> FilterAdvertisementsByCategory(ICategoryService categoryService, int? categoryId, IEnumerable<Advertisement> advertisements)
        {
            var results = advertisements.ToList();
            if(categoryId != null)
            {
                var properCategories = await categoryService.GetAllProperCategoriesForAsync((int)categoryId);
                results = advertisements.Where(a => properCategories.Any(c => c.Id == a.CategoryId)).ToList();
            }
            return results;
        }

        public static List<Advertisement> FilterAdvertisementsByProperties(IEnumerable<Advertisement> advertisements, IEnumerable<PropertyWithValue> propertyInputs)
        {
            var PropertyInputs = propertyInputs.Where(p => p.Value != "false" && p.Value != null).ToList();
            return advertisements.Where(a => PropertyInputs.All(pi =>
                                        a.AdvertisementProperties.Any(ap => ap.Property.Id == pi.Property.Id && ap.Value == pi.Value))).ToList();
        }
    }
}
