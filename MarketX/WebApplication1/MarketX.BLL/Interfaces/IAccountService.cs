using MarketX.BLL.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarketX.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterUser(User user);
        Task<SignInResult> LoginUser(LoginUser user);
        Task LogoutUser();
    }
}
