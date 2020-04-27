using AutoMapper;
using MarketX.BLL.DTOs;
using MarketX.BLL.Interfaces;
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

        public AccountService(UserManager<DAL.Entities.User> userManager, SignInManager<DAL.Entities.User> signInManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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
    }
}
