using AutoMapper;
using MarketX.BLL.DTOs;
using MarketX.BLL.Interfaces;
using MarketX.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketX.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly MarketXContext _context;
        private readonly IMapper _mapper;
        public UserService(MarketXContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> GetUserAsync(int userId)
        {
            var dbUser = await _context.ShopUsers
                .Include(u => u.City)
                    .ThenInclude(c => c.County)
                .FirstOrDefaultAsync(u => u.Id == userId);
            return _mapper.Map<User>(dbUser);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var dbUser = await _context.Users
                .Include(u => u.City)
                .Include(u => u.County)
                .Where(u => u.Email == email).FirstOrDefaultAsync();
            return _mapper.Map<User>(dbUser);
        }

        public async Task<string?> GetUserPhoneNumberAsync(int userId)
        {
            return await _context.ShopUsers.
                Where(u => u.Id == userId).
                Select(u => u.PhoneNumber)
                .SingleOrDefaultAsync();
        }
    }
}
