
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public async Task<IActionResult> CreateAdvertisement()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = await _userService.GetUserIdByEmailAsync(User.Identity.Name!);
                var user = await _userService.GetUserAsync(userId);
                AdvertisementForm advertisementForm = new AdvertisementForm { CityId = user.City?.Id };
                return View(advertisementForm);
            }
            else
                return RedirectToAction("Login", "Account");
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateAdvertisement(AdvertisementForm model)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
            {
                return View("CreateAdvertisement",model);
            }

            Advertisement advertisement = await ParseAdvertisementAsync(model);

            advertisement = await _advertisementService.InsertAdvertisementAsync(advertisement);
            await UploadImages(model.Images);

            return Redirect($"{advertisement.Id}");
        }

        [HttpGet("EditAdvertisement/{id}")]
        public async Task<IActionResult> EditAdvertisement(int id)
        {
            var advertisement = await _advertisementService.GetAdvertisementAsync(id);
            EditAdvertisementForm model = new EditAdvertisementForm
            {
                Id = advertisement.Id,
                Title = advertisement.Title,
                Price = advertisement.Price,
                DaysToLive = advertisement.DaysToLive,
                Description = advertisement.Description,
                IsBuying = advertisement.SellerId == null ? false : true,
                IsUsed = advertisement.Condition == DAL.Entities.Condition.Used ? true : false,
                CategoryId = advertisement.CategoryId,
                CityId = advertisement.CityId,
                PropertyInputs = advertisement.AdvertisementProperties,
                OriginalImagePaths = advertisement.AdvertisementImagePaths
            };
            return View(model);
        }

        [HttpPost("UpdateAdvertisement")]
        public async Task<IActionResult> UpdateAdvertisement(EditAdvertisementForm advertisementForm)
        {
            var advertisement = await ParseAdvertisementAsync(advertisementForm);

            await UploadImages(advertisementForm.Images);

            await _advertisementService.UpdateAdvertisementAsync(advertisementForm.Id, advertisement);
            return Redirect($"{advertisement.Id}");
        }

        private async Task<Advertisement> ParseAdvertisementAsync(AdvertisementForm model)
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

            string userName = User.Identity.Name!;
            int userId = await _userService.GetUserIdByEmailAsync(userName);

            if ((bool)model.IsBuying!)
                advertisement.CustomerId = userId;
            else
                advertisement.SellerId = userId;

            SetPropertiesToAdvertisement(advertisement, model.PropertyInputs);

            if (model.Images.Any())
            {
                foreach (var image in model.Images)
                {
                    var imagePath = Path.Combine(@"~/images/advertisementPhotos", Path.GetFileName(image.FileName));
                    advertisement.AdvertisementImagePaths.Add(imagePath);
                }
            }
            else
            {
                foreach (var image in model.OriginalImagePaths)
                {
                    var imagePath = Path.Combine(@"~/images/advertisementPhotos", Path.GetFileName(image));
                    advertisement.AdvertisementImagePaths.Add(imagePath);
                }
            }

            return advertisement;
        }

        private void SetPropertiesToAdvertisement(Advertisement advertisement, List<PropertyWithValue> properties)
        {
            foreach (var prop in properties)
                if(prop.Value != null)
                    advertisement.AdvertisementProperties.Add(new PropertyWithValue(prop.Property) { Id = prop.Id, Value = prop.Value, PropertyId = prop.Property.Id });
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