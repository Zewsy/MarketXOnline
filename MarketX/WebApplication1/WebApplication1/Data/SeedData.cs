using MarketX.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MarketXContext(
                serviceProvider.GetRequiredService<DbContextOptions<MarketXContext>>())
            )
            {
                if (context.Advertisements.Any())
                    return;

                context.Counties.AddRange(
                    new County
                    {
                        Name = "Budapest"
                    },
                    new County
                    {
                        Name = "Pest megye"
                    },
                    new County
                    {
                        Name = "Jász-Nagykun-Szolnok megye"
                    }
                );

                context.SaveChanges();

                context.Cities.AddRange(
                    new City
                    {
                        Name = "XI. Kerület",
                        CountyID = 1
                    },
                    new City
                    {
                        Name = "Maglód",
                        CountyID = 2
                    },
                    new City
                    {
                        Name = "Jászárokszállás",
                        CountyID = 3
                    }
                );

                context.SaveChanges();

                context.Users.AddRange(
                    new User
                    {
                        FirstName = "János",
                        LastName = "Kovács",
                        Email = "kovacsjanos@asd.hu",
                        Password = "12345",
                        PhoneNumber = "+36705556666",
                        RegistrationDate = new DateTime(1990, 03, 10),
                        CountyID = 3,
                        CityID = 3,
                        ProfilePicturePath = "C:\\Bme\\Önlab\\MarketX\\WebApplication1\\WebApplication1\\wwwroot\\images\\profilePictures\\KovacsJanos.jpg"
                    },
                    new User
                    {
                        FirstName = "Ferenc",
                        LastName = "Kis",
                        Email = "kisferenc@asd.hu",
                        Password = "kisferike5",
                        PhoneNumber = "+36101112222",
                        RegistrationDate = new DateTime(1998, 07, 19),
                        CountyID = 1,
                        CityID = 1,
                        ProfilePicturePath = "C:\\Bme\\Önlab\\MarketX\\WebApplication1\\WebApplication1\\wwwroot\\images\\profilePictures\\KisFerenc.jpg"
                    },
                    new User
                    {
                        FirstName = "Mirjam",
                        LastName = "Lippai",
                        Email = "andika@asd.hu",
                        Password = "asd123",
                        RegistrationDate = new DateTime(1999, 02, 10),
                        CountyID = 2,
                        CityID = 2
                    }
                );

                context.SaveChanges();

                context.Categories.AddRange(
                    new Category
                    {
                        Name = "Ruházat"
                    },
                    new Category
                    {
                        Name = "Uniszex ruházat",
                        ParentCategoryID = 1
                    },
                    new Category
                    {
                        Name = "Uniszex cipő",
                        ParentCategoryID = 2
                    },
                    new Category
                    {
                        Name = "Autó, motor, alkatrész"
                    },
                    new Category
                    {
                        Name = "Személygépkocsi",
                        ParentCategoryID = 4
                    },
                    new Category
                    {
                        Name = "Ingatlan"
                    },
                    new Category
                    {
                        Name = "Lakás",
                        ParentCategoryID = 6
                    }
                );

                context.SaveChanges();

                context.Advertisements.AddRange(
                    new Advertisement
                    {
                        Title = "Eladó túrabakancs",
                        Price = 20000,
                        IsPriorized = true,
                        CreatedDate = new DateTime(2020, 03, 11),
                        DaysToLive = 30,
                        Description = "Kihasználatlanság miatt eladó egy egyszer felvett, tehát gyakorlatilag új, profi túrabakancs.",
                        Condition = Condition.New,
                        Status = Status.Active,
                        SellerID = 3,
                        CityID = 2,
                        CategoryID = 3
                    },
                    new Advertisement
                    {
                        Title = "Audi A6",
                        Price = 5500000,
                        IsPriorized = false,
                        CreatedDate = new DateTime(2020, 03, 13),
                        DaysToLive = 60,
                        Description = "Balesetmentes, garázsban tartott, szakszervízben szervizelt, szervízszámlákkal és szervízkönyvvel, tökéletes állapotban.",
                        Condition = Condition.Used,
                        Status = Status.Active,
                        SellerID = 1,
                        CityID = 1,
                        CategoryID = 5
                    },
                    new Advertisement
                    {
                        Title = "Lakást keresek",
                        IsPriorized = false,
                        CreatedDate = new DateTime(2020, 03, 12),
                        DaysToLive = 30,
                        Description = "Lakást keresek a XI. kerület és környékén.",
                        Condition = Condition.Used,
                        Status = Status.Active,
                        CustomerID = 2,
                        CityID = 1,
                        CategoryID = 7
                    }
                );

                context.SaveChanges();

                context.AdvertisementPhotos.AddRange(
                    new AdvertisementPhoto
                    {
                        ImagePath = "C:\\Bme\\Önlab\\MarketX\\WebApplication1\\WebApplication1\\wwwroot\\images\\advertisementPhotos\\audi.jpg",
                        AdvertisementID = 2
                    },
                    new AdvertisementPhoto
                    {
                        ImagePath = "C:\\Bme\\Önlab\\MarketX\\WebApplication1\\WebApplication1\\wwwroot\\images\\advertisementPhotos\\turabakancs.jpg",
                        AdvertisementID = 1
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
