using AutoMapper;
using MarketX.BLL.DTOs;
using MarketX.BLL.Interfaces;
using MarketX.DAL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarketX.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<DAL.Entities.User> userManager;
        private readonly SignInManager<DAL.Entities.User> signInManager;
        private readonly IMapper _mapper;
        private readonly MarketXContext _context;

        public AccountService(UserManager<DAL.Entities.User> userManager, SignInManager<DAL.Entities.User> signInManager, IMapper mapper, MarketXContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
            _mapper = mapper;
        }

        public async Task<SignInResult> LoginUser(LoginUser user)
        {
            return await signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);
        }

        public async Task LogoutUser()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> RegisterUser(User user)
        {
            DAL.Entities.User dbUser = _mapper.Map<DAL.Entities.User>(user);
            dbUser.UserName = user.Email;
            dbUser.City = null!;
            dbUser.County = null!;
            dbUser.CityId = user.City?.Id;
            dbUser.CountyId = user.County?.Id;
            return await userManager.CreateAsync(dbUser, user.Password);
        }

        public async Task<bool> CheckPassword(int userId, string password)
        {
            var dbUser = await userManager.FindByIdAsync(userId.ToString());
            return await userManager.CheckPasswordAsync(dbUser, password);
        }

        public async Task UpdateUserAsync(int userId, User user)
        {
            var dbUser = await userManager.FindByIdAsync(userId.ToString());

            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;
            dbUser.Email = user.Email;
            dbUser.CityId = user.CityId;
            dbUser.CountyId = user.CountyId;
            dbUser.PhoneNumber = user.PhoneNumber;
            dbUser.UserName = user.Email;
            dbUser.ProfilePicturePath = user.ProfilePicturePath;

            if (user.ProfilePicturePath != null)
                dbUser.ProfilePicturePath = user.ProfilePicturePath;

            await userManager.UpdateAsync(dbUser);
            await signInManager.RefreshSignInAsync(dbUser);
        }

        public async Task ChangeUserPasswordAsync(int userId, string oldPassword, string newPassword)
        {
            var dbUser = await userManager.FindByIdAsync(userId.ToString());
            if (newPassword != oldPassword)
                await userManager.ChangePasswordAsync(dbUser, oldPassword, newPassword);
        }
    }
}
