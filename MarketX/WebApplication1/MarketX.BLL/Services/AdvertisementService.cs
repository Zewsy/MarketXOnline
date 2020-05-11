using AutoMapper;
using MarketX.BLL.DTOs;
using MarketX.BLL.Interfaces;
using MarketX.BLL.Utils;
using MarketX.DAL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.BLL.Services
{
    public enum SortOrder
    {
        DescendingByPrice,
        AscendingByPrice,
        Latest,
        AscendingByABC
    }
    public class AdvertisementService : IAdvertisementService
    {
        private readonly MarketXContext _context;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;

        public AdvertisementService(MarketXContext context, IMapper mapper, ICategoryService categoryService)
        {
            _context = context;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public async Task<Advertisement> GetAdvertisementAsync(int advertisementId)
        {
            var dbAdvertisement = await _context.Advertisements
                                    .Include(a => a.Seller)
                                    .Include(a => a.Customer)
                                    .Include(a => a.AdvertisementPhotos)
                                    .Include(a => a.AdvertisementProperties)
                                        .ThenInclude(ap => ap.Property)
                                            .ThenInclude(p => p.PropertyValues)
                                    .Include(a => a.City)
                                        .ThenInclude(c => c.County)
                                    .Where(a => a.Id == advertisementId)
                                    .SingleOrDefaultAsync();
            var advertisement = _mapper.Map<Advertisement>(dbAdvertisement);
            return advertisement;
        }

        public async Task<List<Advertisement>> GetAdvertisementsForUserAsync(string userName)
        {
            var dbAdvertisements = await _context.Advertisements
                                    .Include(a => a.AdvertisementPhotos)
                                    .Include(a => a.City)
                                        .ThenInclude(a => a.County)
                                    .Include(a => a.Seller)
                                    .Include(a => a.Customer)
                                    .Where(a => a.Seller.Email == userName || a.Customer.Email == userName)
                                    .ToListAsync();
            return _mapper.Map<List<Advertisement>>(dbAdvertisements);
        }

        public async Task<List<Advertisement>> GetAdvertisementsAsync(SearchModel searchModel, int? adsToTake = null)
        {
            var dbAdvertisements = AdvertisementFinder.FilterAdvertisements(_context, searchModel);
            var resultAdvertisements = _mapper.Map<List<Advertisement>>(await dbAdvertisements.ToListAsync());
           
            resultAdvertisements = await AdvertisementFinder.FilterAdvertisementsByCategory(_categoryService, searchModel.CategoryId, resultAdvertisements);
            resultAdvertisements = AdvertisementFinder.FilterAdvertisementsByProperties(resultAdvertisements, searchModel.PropertyInputs);

            if (adsToTake != null)
                resultAdvertisements = resultAdvertisements.Take((int)adsToTake).ToList();

            return resultAdvertisements;
        }

        public async Task<Advertisement> InsertAdvertisementAsync(Advertisement advertisement)
        {
            DAL.Entities.Advertisement dbAdvertisement = _mapper.Map<DAL.Entities.Advertisement>(advertisement);
            _context.Advertisements.Add(dbAdvertisement);
            
            await _context.SaveChangesAsync();

            return _mapper.Map<Advertisement>(dbAdvertisement);
        }

        public async Task DeleteAdvertisementAsync(int id)
        {
            DAL.Entities.Advertisement advertisement = _context.Advertisements.FirstOrDefault(a => a.Id == id);
            if(advertisement != null)
            {
                _context.Advertisements.Remove(advertisement);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAdvertisementAsync(int advertisementId, Advertisement advertisement)
        {
            advertisement.Id = advertisementId;
            var dbAdvertisement = _mapper.Map<DAL.Entities.Advertisement>(advertisement);

            var dbAdvertisementEntity = _context.Advertisements.Attach(dbAdvertisement);
            dbAdvertisementEntity.State = EntityState.Modified;

            var dbImages = await _context.AdvertisementPhotos.Where(ap => ap.AdvertisementID == advertisementId).ToListAsync();
            foreach (var image in dbImages)
            {
                _context.AdvertisementPhotos.Remove(image);
                if (!advertisement.AdvertisementImagePaths.Contains(Path.Combine(@"~/images/advertisementPhotos", Path.GetFileName(image.ImagePath)))){
                    File.Delete(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images/advertisementPhotos", Path.GetFileName(image.ImagePath)));
                }
            }

            foreach (var dbAp in dbAdvertisement.AdvertisementProperties)
            {
                var dbEntity = _context.AdvertisementProperties.Attach(dbAp);
                dbEntity.State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Advertisement>> GetNewAdvertisementsAsync()
        {
            var dbAdvertisements = await _context.Advertisements
                                    .Include(a => a.AdvertisementPhotos)
                                    .Include(a => a.City)
                                        .ThenInclude(a => a.County)
                                    .Include(a => a.Seller)
                                    .Include(a => a.Customer)
                                    .Where(a => a.Status == DAL.Entities.Status.New)
                                    .ToListAsync();
            return _mapper.Map<List<Advertisement>>(dbAdvertisements);
        }

        public async Task ApproveAdvertisementAsync(int advertisementId)
        {
            var dbAd = await _context.Advertisements.FirstAsync(a => a.Id == advertisementId);
            dbAd.Status = DAL.Entities.Status.Active;
            await _context.SaveChangesAsync();
        }
    }
}
