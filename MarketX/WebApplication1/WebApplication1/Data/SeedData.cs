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
                        CountyID = context.Counties.Single(c => c.Name == "Budapest").ID
                    },
                    new City
                    {
                        Name = "Maglód",
                        CountyID = context.Counties.Single(c => c.Name == "Pest megye").ID
                    },
                    new City
                    {
                        Name = "Jászárokszállás",
                        CountyID = context.Counties.Single(c => c.Name == "Jász-Nagykun-Szolnok megye").ID
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
                        CountyID = context.Counties.Single(c => c.Name == "Jász-Nagykun-Szolnok megye").ID,
                        CityID = context.Cities.Single(c => c.Name == "Jászárokszállás").ID,
                        ProfilePicturePath = "~/images/profilePictures/KovacsJanos.jpg"
                    },
                    new User
                    {
                        FirstName = "Ferenc",
                        LastName = "Kis",
                        Email = "kisferenc@asd.hu",
                        Password = "kisferike5",
                        PhoneNumber = "+36101112222",
                        RegistrationDate = new DateTime(1998, 07, 19),
                        CountyID = context.Counties.Single(c => c.Name == "Budapest").ID,
                        CityID = context.Cities.Single(c => c.Name == "XI. Kerület").ID,
                        ProfilePicturePath = "~/images/profilePictures/KisFerenc.jpg"
                    },
                    new User
                    {
                        FirstName = "Mirjam",
                        LastName = "Lippai",
                        Email = "andika@asd.hu",
                        Password = "asd123",
                        RegistrationDate = new DateTime(1999, 02, 10),
                        CountyID = context.Counties.Single(c => c.Name == "Pest megye").ID,
                        CityID = context.Cities.Single(c => c.Name == "Maglód").ID
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
                        Name = "Autó, motor, alkatrész"
                    },
                    new Category
                    {
                        Name = "Ingatlan"
                    },
                    new Category
                    {
                        Name = "Számítástechnika"
                    }                   
                );

                context.SaveChanges();

                context.AddRange(
                    new Category
                    {
                        Name = "Uniszex ruházat",
                        ParentCategoryID = context.Categories.Single(c => c.Name=="Ruházat").ID
                    }, 
                    new Category
                    {
                        Name = "Személygépkocsi",
                        ParentCategoryID = context.Categories.Single(c => c.Name == "Autó, motor, alkatrész").ID
                    },
                    new Category
                    {
                        Name = "Lakás",
                        ParentCategoryID = context.Categories.Single(c => c.Name == "Ingatlan").ID
                    },
                     new Category
                     {
                         Name = "Laptop, notebook",
                         ParentCategoryID = context.Categories.Single(c => c.Name == "Számítástechnika").ID
                     }
                 );

                context.SaveChanges();

                context.Categories.Add(new Category
                {
                    Name = "Uniszex cipő",
                    ParentCategoryID = context.Categories.Single(c => c.Name == "Uniszex ruházat").ID
                });

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
                        SellerID = context.Users.Single(u => u.FirstName=="János").ID,
                        CityID = context.Cities.Single(c => c.Name == "Maglód").ID,
                        CategoryID = context.Categories.Single(c => c.Name == "Uniszex cipő").ID
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
                        SellerID = context.Users.Single(u => u.FirstName == "János").ID,
                        CityID = context.Cities.Single(c => c.Name == "XI. Kerület").ID,
                        CategoryID = context.Categories.Single(c => c.Name == "Személygépkocsi").ID
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
                        CustomerID = context.Users.Single(u => u.FirstName == "Ferenc").ID,
                        CityID = context.Cities.Single(c => c.Name == "XI. Kerület").ID,
                        CategoryID = context.Categories.Single(c => c.Name == "Lakás").ID
                    },
                    new Advertisement
                    {
                        Title = "Lakás eladó",
                        Price = 60000000,
                        IsPriorized = false,
                        CreatedDate = new DateTime(2020, 03, 12),
                        DaysToLive = 30,
                        Description = "Lakás eladó a XI. kerületben.",
                        Condition = Condition.Used,
                        Status = Status.Active,
                        SellerID = context.Users.Single(u => u.FirstName == "János").ID,
                        CityID = context.Cities.Single(c => c.Name == "XI. Kerület").ID,
                        CategoryID = context.Categories.Single(c => c.Name == "Lakás").ID
                    },
                    new Advertisement
                    {
                        Title = "Opel Insignia eladó",
                        Price = 9000000,
                        IsPriorized = false,
                        CreatedDate = new DateTime(2020, 03, 12),
                        DaysToLive = 30,
                        Description = "Balesetmentes, garázsban tartott, szakszervízben szervizelt, szervízszámlákkal és szervízkönyvvel, tökéletes állapotban.",
                        Condition = Condition.Used,
                        Status = Status.Active,
                        SellerID = context.Users.Single(u => u.FirstName == "Mirjam").ID,
                        CityID = context.Cities.Single(c => c.Name == "Jászárokszállás").ID,
                        CategoryID = context.Categories.Single(c => c.Name == "Személygépkocsi").ID
                    },
                    new Advertisement
                    {
                        Title = "Acer Predator",
                        Price = 800000,
                        IsPriorized = false,
                        CreatedDate = new DateTime(2020, 03, 12),
                        DaysToLive = 30,
                        Description = "Új laptop eladó.",
                        Condition = Condition.New,
                        Status = Status.Active,
                        SellerID = context.Users.Single(u => u.FirstName == "János").ID,
                        CityID = context.Cities.Single(c => c.Name == "Maglód").ID,
                        CategoryID = context.Categories.Single(c => c.Name == "Laptop, notebook").ID
                    },
                    new Advertisement
                    {
                        Title = "Asus Rog",
                        Price = 600000,
                        IsPriorized = false,
                        CreatedDate = new DateTime(2020, 03, 12),
                        DaysToLive = 30,
                        Description = "Új laptop eladó.",
                        Condition = Condition.New,
                        Status = Status.Active,
                        SellerID = context.Users.Single(u => u.FirstName == "Mirjam").ID,
                        CityID = context.Cities.Single(c => c.Name == "XI. Kerület").ID,
                        CategoryID = context.Categories.Single(c => c.Name == "Laptop, notebook").ID
                    }
                );

                context.SaveChanges();

                context.AdvertisementPhotos.AddRange(
                    new AdvertisementPhoto
                    {
                        ImagePath = "~/images/advertisementPhotos/audi.jpg",
                        AdvertisementID = context.Advertisements.Single(a => a.Title== "Audi A6").ID
                    },
                    new AdvertisementPhoto
                    {
                        ImagePath = "~/images/advertisementPhotos/turabakancs.jpg",
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Eladó túrabakancs").ID
                    },
                    new AdvertisementPhoto
                    {
                        ImagePath = "~/images/advertisementPhotos/flat.jpg",
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Lakás eladó").ID
                    },
                    new AdvertisementPhoto
                    {
                        ImagePath = "~/images/advertisementPhotos/opel.jpg",
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Opel Insignia eladó").ID
                    },
                    new AdvertisementPhoto
                    {
                        ImagePath = "~/images/advertisementPhotos/acer.jpg",
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Acer Predator").ID
                    },
                    new AdvertisementPhoto
                    {
                        ImagePath = "~/images/advertisementPhotos/asus.jpg",
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Asus Rog").ID
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
