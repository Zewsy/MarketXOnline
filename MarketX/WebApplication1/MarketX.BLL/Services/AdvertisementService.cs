using AutoMapper;
using MarketX.BLL.DTOs;
using MarketX.BLL.Interfaces;
using MarketX.BLL.Utils;
using MarketX.DAL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
                                    .Include(a => a.City)
                                        .ThenInclude(c => c.County)
                                    .Where(a => a.Id == advertisementId)
                                    .SingleOrDefaultAsync();

            return _mapper.Map<Advertisement>(dbAdvertisement);
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
            _context.AdvertisementProperties.AttachRange(dbAdvertisement.AdvertisementProperties);
            _context.Advertisements.Add(dbAdvertisement);
            
            await _context.SaveChangesAsync();

            return _mapper.Map<Advertisement>(dbAdvertisement);
        }
    }
}
