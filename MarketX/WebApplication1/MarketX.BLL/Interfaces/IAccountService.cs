using MarketX.BLL.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace MarketX.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterUser(User user);
        Task<SignInResult> LoginUser(LoginUser user);
        Task LogoutUser();
        Task UpdateUserAsync(int userId, User user);
        Task ChangeUserPasswordAsync(int userId, string oldPassword, string newPassword);
        Task<bool> CheckPassword(int userId, string password);
    }
}
