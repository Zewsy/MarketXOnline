using MarketX.BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketX.BLL.Interfaces
{
    public interface IAdvertisementService
    {
        Task<Advertisement> GetAdvertisementAsync(int advertisementId);
        Task<List<Advertisement>> GetAdvertisementsAsync(SearchModel searchModel, int? adsToTake = null);
        Task<Advertisement> InsertAdvertisementAsync(Advertisement advertisement);
    }
}
