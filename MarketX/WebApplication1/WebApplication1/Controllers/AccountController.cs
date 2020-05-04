using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MarketX.BLL.DTOs;
using MarketX.BLL.Interfaces;
using MarketX.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MarketX.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IAdvertisementService _advertisementService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IAdvertisementService advertisementService, IUserService userService, IMapper mapper)
        {
            _accountService = accountService;
            _advertisementService = advertisementService;
            this._userService = userService;
            this._mapper = mapper;
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User(
                    registerViewModel.FirstName!,
                    registerViewModel.LastName!,
                    registerViewModel.Password!,
                    registerViewModel.Email!,
                    registrationDate: DateTime.Now);

                if (registerViewModel.CountyId != null) user.County = new County { Id = (int)registerViewModel.CountyId };
                if (registerViewModel.CityId != null) user.City = new City { Id = (int)registerViewModel.CityId };
                if (registerViewModel.ProfilePicture != null)
                {
                    await UploadImage(registerViewModel.ProfilePicture);
                    var imagePath = Path.Combine(@"~/images/profilePictures", Path.GetFileName(registerViewModel.ProfilePicture.FileName));
                    user.ProfilePicturePath = imagePath;
                }

                var result = await _accountService.RegisterUser(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginUser loginUser)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.LoginUser(loginUser);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Sikertelen belépés.");
            }
            return View(loginUser);
        }

        [HttpPost]
        public async Task<IActionResult> LogoutAsync()
        {
            await _accountService.LogoutUser();
            return RedirectToAction("Index", "Home");
        }

        private async Task UploadImage(IFormFile image)
        {
            var fileName = Path.GetFileName(image.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images/profilePictures", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
        }

        [HttpGet("{userName}/Advertisements")]
        public async Task<IActionResult> UserAdvertisements(string userName)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (User.Identity.Name != userName)
                return RedirectToAction("Index", "Home");


            var advertisements = await _advertisementService.GetAdvertisementsForUserAsync(userName);
            var advertisementCards = advertisements.Select(a => new ResultAdvertisementCard
            {
                ID = a.Id,
                City = a.City.Name,
                County = a.City.County!.Name,
                AdType = a.SellerId == null ? AdType.Buying : AdType.Selling,
                ImagePath = a.AdvertisementImagePaths.Any() ? a.AdvertisementImagePaths.First() : Url.Content("~/images/image-placeholder.jpg"),
                Title = a.Title,
                Price = a.Price,
                Status = a.Status,
                UserName = a.Seller == null ? a.Customer!.LastName + " " + a.Customer.FirstName : a.Seller.LastName + " " + a.Seller.FirstName
            }).ToList();

            var activeAdvertisements = advertisementCards.Where(a => a.Status == DAL.Entities.Status.Active).ToList();
            var newAdvertisements = advertisementCards.Where(a => a.Status == DAL.Entities.Status.New).ToList();

            var result = new UserAdvertisementsViewModel()
            {
                ActiveAdvertisements = activeAdvertisements,
                NewAdvertisements = newAdvertisements
            };

            return View(result);
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> UserProfile(string userName)
        {
            int userId = await _userService.GetUserIdByEmailAsync(userName);
            var dbUser = await _userService.GetUserAsync(userId);
            var user = _mapper.Map<User>(dbUser);
            return View(user);
        }

        [HttpDelete("DeleteAdvertisement/{id}")]
        public async Task<IActionResult> DeleteAdvertisement(int id)
        {
            await _advertisementService.DeleteAdvertisementAsync(id);
            return NoContent();
        }
    }
}