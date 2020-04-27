using MarketX.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarketX.BLL.Interfaces
{
    public interface ICityCountyService
    {
        Task<IEnumerable<City>> GetCitiesInCountyAsync(int countyId);
        Task<IEnumerable<County>> GetCountiesAsync();
    }
}
