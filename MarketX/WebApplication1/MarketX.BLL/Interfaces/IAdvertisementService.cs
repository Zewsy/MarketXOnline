using MarketX.BLL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketX.BLL.Interfaces
{
    public interface IAdvertisementService
    {
        Task<Advertisement> GetAdvertisementAsync(int advertisementId);
        Task<List<Advertisement>> GetNewAdvertisementsAsync();
        Task<List<Advertisement>> GetAdvertisementsAsync(SearchModel searchModel, int? adsToTake = null);
        Task<List<Advertisement>> GetAdvertisementsForUserAsync(string userName);
        Task<Advertisement> InsertAdvertisementAsync(Advertisement advertisement);
        Task DeleteAdvertisementAsync(int id);
        Task UpdateAdvertisementAsync(int advertisementId, Advertisement advertisement);
        Task ApproveAdvertisementAsync(int advertisementId);
    }
}
