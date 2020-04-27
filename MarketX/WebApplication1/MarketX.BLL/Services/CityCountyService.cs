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
    public class CityCountyService : ICityCountyService
    {
        private readonly MarketXContext _context;
        private readonly IMapper _mapper;

        public CityCountyService(MarketXContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<City>> GetCitiesInCountyAsync(int countyId)
        {
            var dbCities = await _context.Counties.Where(c => c.Id == countyId).Select(c => c.Cities).FirstOrDefaultAsync();
            return _mapper.Map<List<City>>(dbCities);
        }

        public async Task<IEnumerable<County>> GetCountiesAsync()
        {
            var dbCounties = await _context.Counties.ToListAsync();
            return _mapper.Map<List<County>>(dbCounties);
        }
    }
}
