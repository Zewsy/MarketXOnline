using MarketX.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarketX.BLL.Interfaces
{
    public interface IUserService
    {
        Task<string?> GetUserPhoneNumberAsync(int userId);
        Task<User> GetUserAsync(int userId);

        Task<int> GetUserIdByEmailAsync(string email);
    }
}
