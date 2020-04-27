using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MarketX.BLL.DTOs;
using MarketX.BLL.Interfaces;
using MarketX.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MarketX.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
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
                    // await signInManager.SignInAsync(user, isPersistent: false); TODO: Login
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
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
    }
}