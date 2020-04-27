
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MarketX.BLL.DTOs;
using MarketX.BLL.Interfaces;
using MarketX.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketX.Controllers
{
    [Route("Advertisement")]
    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly IUserService _userService;
        public AdvertisementController(IAdvertisementService advertisementService, IUserService userService)
        {
            _advertisementService = advertisementService;
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Advertisement(int id)
        {
            var advertisement = await _advertisementService.GetAdvertisementAsync(id);

            if(advertisement != null)
                return View(advertisement);

            return NotFound();
        }

        [HttpGet("UserPhoneNumber/{id}")]
        public async Task<string?> UserPhoneNumber(int id)
        {
            return await _userService.GetUserPhoneNumberAsync(id);
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

            advertisement = await _advertisementService.InsertAdvertisementAsync(advertisement);
            await UploadImages(model.Images);

            return Redirect($"{advertisement.Id}");
        }

        private Advertisement ParseAdvertisement(CreateAdvertisementForm model)
        {
            Advertisement advertisement = new Advertisement(
                title: model.Title!,
                isPriorized: false,
                createdDate: DateTime.Now,
                daysToLive: (int)model.DaysToLive!,
                description: model.Description!,
                condition: (bool)model.IsUsed! ? DAL.Entities.Condition.Used : DAL.Entities.Condition.New,
                status: DAL.Entities.Status.New,
                categoryId: (int)model.CategoryId!,
                cityId: (int)model.CityId!)
            {
                Price = model.Price
            };

            int userId = 1;   //TODO

            if ((bool)model.IsBuying!)
                advertisement.CustomerId = userId;
            else
                advertisement.SellerId = userId;

            SetPropertiesToAdvertisement(advertisement, model.PropertyInputs);

            foreach (var image in model.Images)
            {
                var imagePath = Path.Combine(@"~/images/advertisementPhotos", Path.GetFileName(image.FileName));
                advertisement.AdvertisementImagePaths.Add(imagePath);
            }

            return advertisement;
        }

        private void SetPropertiesToAdvertisement(Advertisement advertisement, List<PropertyWithValue> properties)
        {
            foreach (var prop in properties)
                if(prop.Value != null)
                    advertisement.AdvertisementProperties.Add(new PropertyWithValue(prop.Property) { Value = prop.Value });
        }

        private async Task UploadImages(List<IFormFile> images)
        {
            foreach(var image in images)
            {
                if(image.Length > 0)
                {
                    await UploadImage(image);
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
    }
}