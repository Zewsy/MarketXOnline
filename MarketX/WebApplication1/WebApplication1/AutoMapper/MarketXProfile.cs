using AutoMapper;
using System.Linq;

namespace MarketX
{
    public class MarketXProfile : Profile
    {
        public MarketXProfile()
        {
            CreateMap<DAL.Entities.Advertisement, BLL.DTOs.Advertisement>()
                .ForMember(dto => dto.AdvertisementImagePaths, opt => opt.Ignore())
                .AfterMap((a, dto, ctx) => 
                    dto.AdvertisementImagePaths = a.AdvertisementPhotos.Select(ap => ap.ImagePath).ToList())
                .ReverseMap();
            CreateMap<DAL.Entities.AdvertisementProperty, BLL.DTOs.PropertyWithValue>().ReverseMap();
            CreateMap<DAL.Entities.Property, BLL.DTOs.Property>().ReverseMap();
            CreateMap<DAL.Entities.PropertyValue, BLL.DTOs.PropertyValue>().ReverseMap();
            CreateMap<DAL.Entities.City, BLL.DTOs.City>().ReverseMap();
            CreateMap<DAL.Entities.County, BLL.DTOs.County>().ReverseMap();
            CreateMap<DAL.Entities.Category, BLL.DTOs.Category>()
                .ForMember(dto => dto.Properties, opt => opt.Ignore())
                .AfterMap((c, dto, ctx) => 
                    dto.Properties = c.CategoryProperties.Select(cp => 
                    ctx.Mapper.Map<BLL.DTOs.Property>(cp.Property)).ToList())
                .ReverseMap();
            CreateMap<DAL.Entities.User, BLL.DTOs.User>()
                .ConstructUsing(dbUser => new BLL.DTOs.User(dbUser.FirstName, dbUser.LastName, dbUser.PasswordHash, dbUser.Email, dbUser.RegistrationDate))
                .ForMember(dto => dto.Password, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
