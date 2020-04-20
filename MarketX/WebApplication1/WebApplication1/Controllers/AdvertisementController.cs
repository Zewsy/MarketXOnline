
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MarketX.Data;
using MarketX.Models;
using MarketX.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketX.Controllers
{
    [Route("Advertisement")]
    public class AdvertisementController : Controller
    {
        private readonly MarketXContext context;
        public AdvertisementController(MarketXContext _context)
        {
            context = _context;
        }


        [HttpGet("{id}")]
        public IActionResult Advertisement(int id)
        {
            var advertisement = context.Advertisements
                                    .Include(a => a.Seller)
                                    .Include(a => a.Customer)
                                    .Include(a => a.AdvertisementPhotos)
                                    .Include(a => a.AdvertisementProperties)
                                        .ThenInclude(ap => ap.Property)
                                    .Include(a => a.City)
                                        .ThenInclude(c => c.County)
                                    .Where(a => a.ID == id)
                                    .FirstOrDefault();

            if(advertisement != null)
                return View(advertisement);

            return NotFound();
        }

        [HttpGet("UserPhoneNumber/{id}")]
        public async Task<string?> UserPhoneNumber(int id)
        {
            return await context.Users.Where(u => u.ID == id).Select(u => u.PhoneNumber).FirstOrDefaultAsync();
        }

        [Route("Create")]
        [HttpGet]
        public IActionResult CreateAdvertisement()
        {
            return View();
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateAdvertisement(CreateAdvertisementForm model)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateAdvertisement",model);
            }

            Advertisement advertisement = ParseAdvertisement(model);

            context.Advertisements.Add(advertisement);
            context.SaveChanges();

            await UploadImages(model.Images, advertisement);

            return Redirect($"{advertisement.ID}");
        }

        private Advertisement ParseAdvertisement(CreateAdvertisementForm model)
        {
            int categoryID = GetCategoryID(model.Category!);
            int cityID = GetCityID(model.City!);

            Advertisement advertisement = new Advertisement(
                title: model.Title!,
                isPriorized: false,
                createdDate: DateTime.Now,
                daysToLive: (int)model.DaysToLive!,
                description: model.Description!,
                condition: (bool)model.IsUsed! ? Condition.Used : Condition.New,
                status: Status.New,
                categoryID: categoryID,
                cityID: cityID
                );

            int userID = context.Users.Where(u => u.ID == 1).First().ID;    //TODO

            if ((bool)model.IsBuying!)
                advertisement.CustomerID = userID;
            else
                advertisement.SellerID = userID;

            SetPropertiesToAdvertisement(advertisement, model.PropertyInputs);

            return advertisement;
        }

        private void SetPropertiesToAdvertisement(Advertisement advertisement, List<PropertyInputField> properties)
        {
            foreach (var prop in properties)
                if(prop.Value != null)
                {
                    advertisement.AdvertisementProperties.Add(new AdvertisementProperty(valueAsString: prop.Value)
                    {
                        PropertyID = GetPropertyID(prop.Name),
                        Advertisement = advertisement
                    });
                }
        }

        private int GetCategoryID(string CategoryName)
        {
            return context.Categories.Where(c => c.Name == CategoryName).First().ID;
        }

        private int GetCityID(string CityName)
        {
            return context.Cities.Where(c => c.Name == CityName).First().ID;
        }

        private int GetPropertyID(string? PropertyName)
        {
            return context.Properties.Where(p => p.Name == PropertyName).FirstOrDefault().ID;
        }

        private async Task UploadImages(List<IFormFile> images, Advertisement advertisement)
        {
            foreach(var image in images)
            {
                if(image.Length > 0)
                {
                    await UploadImage(image);
                    await AddImageToDb(image, advertisement);
                }
            }
        }

        private async Task UploadImage(IFormFile image)
        {
            var fileName = Path.GetFileName(image.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images/advertisementPhotos", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
        }

        private async Task AddImageToDb(IFormFile image, Advertisement advertisement)
        {
            var fileName = Path.GetFileName(image.FileName);
            var filePath = Path.Combine(@"~/images/advertisementPhotos", fileName);
            context.AdvertisementPhotos.Add(
                new AdvertisementPhoto(imagePath: filePath)
                {
                    Advertisement = advertisement
                }
            );
            await context.SaveChangesAsync();
        }
    }
}