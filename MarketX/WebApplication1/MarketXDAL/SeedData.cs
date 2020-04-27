using MarketX.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MarketX.DAL.Entities
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
                    new County(name: "Budapest"),
                    new County(name: "Pest megye"),
                    new County(name: "Jász-Nagykun-Szolnok megye")
                );

                context.SaveChanges();

                context.Cities.AddRange(
                    new City(name: "XI. Kerület")
                    {
                        CountyId = context.Counties.Single(c => c.Name == "Budapest").Id
                    },
                    new City(name: "Maglód")
                    {
                        CountyId = context.Counties.Single(c => c.Name == "Pest megye").Id
                    },
                    new City(name: "Jászárokszállás")
                    {
                        CountyId = context.Counties.Single(c => c.Name == "Jász-Nagykun-Szolnok megye").Id
                    }
                );

                context.SaveChanges();

                context.ShopUsers.AddRange(
                    new User
                    (
                        firstName: "János",
                        lastName: "Kovács",
                        email: "kovacsjanos@asd.hu",
                        registrationDate: new DateTime(1990, 03, 10)
                    )
                    {
                        PhoneNumber = "+36705556666",
                        CountyId = context.Counties.Single(c => c.Name == "Jász-Nagykun-Szolnok megye").Id,
                        CityId = context.Cities.Single(c => c.Name == "Jászárokszállás").Id,
                        ProfilePicturePath = "~/images/profilePictures/KovacsJanos.jpg"
                    },
                    new User
                    (
                        firstName: "Ferenc",
                        lastName: "Kis",
                        email: "kisferenc@asd.hu",
                        registrationDate: new DateTime(1998, 07, 19)
                    )
                    {
                        PhoneNumber = "+36101112222",
                        CountyId = context.Counties.Single(c => c.Name == "Budapest").Id,
                        CityId = context.Cities.Single(c => c.Name == "XI. Kerület").Id,
                        ProfilePicturePath = "~/images/profilePictures/KisFerenc.jpg"
                    },
                    new User
                    (
                        firstName: "Mirjam",
                        lastName: "Lippai",
                        email: "andika@asd.hu",
                        registrationDate: new DateTime(1999, 02, 10)
                    )
                    {
                        CountyId = context.Counties.Single(c => c.Name == "Pest megye").Id,
                        CityId = context.Cities.Single(c => c.Name == "Maglód").Id
                    }
                );

                context.SaveChanges();

                context.Categories.AddRange(
                    new Category(name: "Ruházat"),                   
                    new Category(name: "Autó, motor, alkatrész"),
                    new Category(name: "Ingatlan"),
                    new Category(name: "Számítástechnika")                 
                );

                context.SaveChanges();

                context.AddRange(
                    new Category(name: "Uniszex ruházat")
                    {
                        ParentCategoryId = context.Categories.Single(c => c.Name=="Ruházat").Id
                    }, 
                    new Category(name: "Személygépkocsi")
                    {
                        ParentCategoryId = context.Categories.Single(c => c.Name == "Autó, motor, alkatrész").Id
                    },
                    new Category(name: "Lakás")
                    {
                        ParentCategoryId = context.Categories.Single(c => c.Name == "Ingatlan").Id
                    },
                     new Category(name: "Laptop, notebook")
                     {
                         ParentCategoryId = context.Categories.Single(c => c.Name == "Számítástechnika").Id
                     }
                 );

                context.SaveChanges();

                context.Categories.Add(new Category(name: "Uniszex cipő")
                {
                    ParentCategoryId = context.Categories.Single(c => c.Name == "Uniszex ruházat").Id
                });

                context.SaveChanges();

                context.Advertisements.AddRange(
                    new Advertisement
                    (
                        title: "Eladó túrabakancs",
                        isPriorized: true,
                        createdDate: new DateTime(2020, 03, 11),
                        daysToLive: 30,
                        description: "Kihasználatlanság miatt eladó egy egyszer felvett, tehát gyakorlatilag új, profi túrabakancs.",
                        condition: Condition.New,
                        status: Status.Active,
                        cityId: context.Cities.Single(c => c.Name == "Maglód").Id,
                        categoryId: context.Categories.Single(c => c.Name == "Uniszex cipő").Id
                    )
                    {
                        Price = 20000,
                        SellerId = context.ShopUsers.Single(u => u.FirstName == "János").Id,
                    },
                    new Advertisement
                    (
                        title: "Audi A6",
                        isPriorized: false,
                        createdDate: new DateTime(2020, 03, 13),
                        daysToLive: 60,
                        description: "Balesetmentes, garázsban tartott, szakszervízben szervizelt, szervízszámlákkal és szervízkönyvvel, tökéletes állapotban.",
                        condition: Condition.Used,
                        status: Status.Active,
                        cityId: context.Cities.Single(c => c.Name == "XI. Kerület").Id,
                        categoryId: context.Categories.Single(c => c.Name == "Személygépkocsi").Id
                    )
                    {
                        Price = 5500000,
                        SellerId = context.ShopUsers.Single(u => u.FirstName == "János").Id
                    },
                    new Advertisement
                    (
                        title: "Lakást keresek",
                        isPriorized: false,
                        createdDate: new DateTime(2020, 03, 12),
                        daysToLive: 30,
                        description: "Lakást keresek a XI. kerület és környékén.",
                        condition: Condition.Used,
                        status: Status.Active,
                        cityId: context.Cities.Single(c => c.Name == "XI. Kerület").Id,
                        categoryId: context.Categories.Single(c => c.Name == "Lakás").Id
                    )
                    {
                        CustomerId = context.ShopUsers.Single(u => u.FirstName == "Ferenc").Id
                    },
                    new Advertisement
                    (
                        title: "Lakás eladó",
                        isPriorized: false,
                        createdDate: new DateTime(2020, 03, 12),
                        daysToLive: 30,
                        description: "Lakás eladó a XI. kerületben.",
                        condition: Condition.Used,
                        status: Status.Active,
                        cityId: context.Cities.Single(c => c.Name == "XI. Kerület").Id,
                        categoryId: context.Categories.Single(c => c.Name == "Lakás").Id
                    )
                    {
                        Price = 60000000,  
                        SellerId = context.ShopUsers.Single(u => u.FirstName == "János").Id
                    },
                    new Advertisement
                    (
                        title: "Opel Insignia eladó",
                        isPriorized: false,
                        createdDate: new DateTime(2020, 03, 12),
                        daysToLive: 30,
                        description: "Balesetmentes, garázsban tartott, szakszervízben szervizelt, szervízszámlákkal és szervízkönyvvel, tökéletes állapotban.",
                        condition: Condition.Used,
                        status: Status.Active,
                        cityId: context.Cities.Single(c => c.Name == "Jászárokszállás").Id,
                        categoryId: context.Categories.Single(c => c.Name == "Személygépkocsi").Id
                    )
                    {
                        Price = 9000000,
                        SellerId = context.ShopUsers.Single(u => u.FirstName == "Mirjam").Id
                    },
                    new Advertisement
                    (
                        title: "Opel Insignia eladó2",
                        isPriorized: false,
                        createdDate: new DateTime(2020, 03, 12),
                        daysToLive: 30,
                        description: "Balesetmentes, garázsban tartott, szakszervízben szervizelt, szervízszámlákkal és szervízkönyvvel, tökéletes állapotban.",
                        condition: Condition.Used,
                        status: Status.Active,
                        cityId: context.Cities.Single(c => c.Name == "Jászárokszállás").Id,
                        categoryId: context.Categories.Single(c => c.Name == "Személygépkocsi").Id
                    )
                    {
                        Price = 9000000,
                        SellerId = context.ShopUsers.Single(u => u.FirstName == "Mirjam").Id
                    },
                    new Advertisement
                    (
                        title: "Opel Insignia eladó3",
                        isPriorized: false,
                        createdDate: new DateTime(2020, 03, 12),
                        daysToLive: 30,
                        description: "Balesetmentes, garázsban tartott, szakszervízben szervizelt, szervízszámlákkal és szervízkönyvvel, tökéletes állapotban.",
                        condition: Condition.Used,
                        status: Status.Active,
                        cityId: context.Cities.Single(c => c.Name == "Jászárokszállás").Id,
                        categoryId: context.Categories.Single(c => c.Name == "Személygépkocsi").Id
                    )
                    {
                        Price = 9000000,
                        SellerId = context.ShopUsers.Single(u => u.FirstName == "Mirjam").Id
                    },
                    new Advertisement
                    (
                        title: "Opel Insignia eladó4",
                        isPriorized: false,
                        createdDate: new DateTime(2020, 03, 12),
                        daysToLive: 30,
                        description: "Balesetmentes, garázsban tartott, szakszervízben szervizelt, szervízszámlákkal és szervízkönyvvel, tökéletes állapotban.",
                        condition: Condition.Used,
                        status: Status.Active,
                        cityId: context.Cities.Single(c => c.Name == "Jászárokszállás").Id,
                        categoryId: context.Categories.Single(c => c.Name == "Személygépkocsi").Id
                    )
                    {
                        Price = 9000000,
                        SellerId = context.ShopUsers.Single(u => u.FirstName == "Mirjam").Id
                    },
                    new Advertisement
                    (
                        title: "Opel Insignia eladó5",
                        isPriorized: false,
                        createdDate: new DateTime(2020, 03, 12),
                        daysToLive: 30,
                        description: "Balesetmentes, garázsban tartott, szakszervízben szervizelt, szervízszámlákkal és szervízkönyvvel, tökéletes állapotban.",
                        condition: Condition.Used,
                        status: Status.Active,
                        cityId: context.Cities.Single(c => c.Name == "Jászárokszállás").Id,
                        categoryId: context.Categories.Single(c => c.Name == "Személygépkocsi").Id
                    )
                    {
                        Price = 9000000,
                        SellerId = context.ShopUsers.Single(u => u.FirstName == "Mirjam").Id
                    },
                    new Advertisement
                    (
                        title: "Opel Insignia eladó6",
                        isPriorized: false,
                        createdDate: new DateTime(2020, 03, 12),
                        daysToLive: 30,
                        description: "Balesetmentes, garázsban tartott, szakszervízben szervizelt, szervízszámlákkal és szervízkönyvvel, tökéletes állapotban.",
                        condition: Condition.Used,
                        status: Status.Active,
                        cityId: context.Cities.Single(c => c.Name == "Jászárokszállás").Id,
                        categoryId: context.Categories.Single(c => c.Name == "Személygépkocsi").Id
                    )
                    {
                        Price = 9000000,
                        SellerId = context.ShopUsers.Single(u => u.FirstName == "Mirjam").Id
                    },
                    new Advertisement
                    (
                        title: "Mercedes Benz",
                        isPriorized: false,
                        createdDate: new DateTime(2020, 04, 10),
                        daysToLive: 30,
                        description: "Balesetmentes, garázsban tartott, szakszervízben szervizelt, szervízszámlákkal és szervízkönyvvel, tökéletes állapotban.",
                        condition: Condition.Used,
                        status: Status.Active,
                        cityId: context.Cities.Single(c => c.Name == "XI. Kerület").Id,
                        categoryId: context.Categories.Single(c => c.Name == "Személygépkocsi").Id
                    )
                    {
                        Price = 8000000,
                        SellerId = context.ShopUsers.Single(u => u.FirstName == "Mirjam").Id
                    },
                    new Advertisement
                    (
                        title: "Nissan X-Trail",
                        isPriorized: false,
                        createdDate: new DateTime(2020, 03, 12),
                        daysToLive: 30,
                        description: "Balesetmentes, garázsban tartott, szakszervízben szervizelt, szervízszámlákkal és szervízkönyvvel, tökéletes állapotban.",
                        condition: Condition.Used,
                        status: Status.Active,
                        cityId: context.Cities.Single(c => c.Name == "Maglód").Id,
                        categoryId: context.Categories.Single(c => c.Name == "Személygépkocsi").Id
                    )
                    {
                        Price = 7000000,
                        SellerId = context.ShopUsers.Single(u => u.FirstName == "Mirjam").Id
                    },
                    new Advertisement
                    (
                        title: "Ford Fiesta",
                        isPriorized: false,
                        createdDate: new DateTime(2020, 03, 12),
                        daysToLive: 30,
                        description: "Balesetmentes, garázsban tartott, szakszervízben szervizelt, szervízszámlákkal és szervízkönyvvel, tökéletes állapotban.",
                        condition: Condition.Used,
                        status: Status.Active,
                        cityId: context.Cities.Single(c => c.Name == "XI. Kerület").Id,
                        categoryId: context.Categories.Single(c => c.Name == "Személygépkocsi").Id
                    )
                    {
                        Price = 4000000,
                        SellerId = context.ShopUsers.Single(u => u.FirstName == "Mirjam").Id
                    },
                    new Advertisement
                    (
                        title: "Acer Predator",
                        isPriorized: false,
                        createdDate: new DateTime(2020, 03, 12),
                        daysToLive: 30,
                        description: "Új laptop eladó.",
                        condition: Condition.New,
                        status: Status.Active,
                        cityId: context.Cities.Single(c => c.Name == "Maglód").Id,
                        categoryId: context.Categories.Single(c => c.Name == "Laptop, notebook").Id
                    )
                    {                      
                        Price = 800000,
                        SellerId = context.ShopUsers.Single(u => u.FirstName == "János").Id
                    },
                    new Advertisement
                    (
                        title: "Asus Rog",
                        isPriorized: false,
                        createdDate: new DateTime(2020, 03, 12),
                        daysToLive: 30,
                        description: "Új laptop eladó.",
                        condition: Condition.New,
                        status: Status.Active,
                        cityId: context.Cities.Single(c => c.Name == "XI. Kerület").Id,
                        categoryId: context.Categories.Single(c => c.Name == "Laptop, notebook").Id
                    )
                    {
                        Price = 600000,                        
                        SellerId = context.ShopUsers.Single(u => u.FirstName == "Mirjam").Id,
                    }
                );

                context.SaveChanges();

                context.AdvertisementPhotos.AddRange(
                    new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/audi.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title== "Audi A6").Id
                    },
                    new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/audi_2.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Audi A6").Id
                    },
                    new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/audi_3.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Audi A6").Id
                    },
                    new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/turabakancs.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Eladó túrabakancs").Id
                    },
                    new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/flat.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Lakás eladó").Id
                    },
                    new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/opel.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Opel Insignia eladó").Id
                    },
                    new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/acer.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Acer Predator").Id
                    },
                    new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/asus.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Asus Rog").Id
                    },
                    new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/opel.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Opel Insignia eladó2").Id
                    }, new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/opel.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Opel Insignia eladó3").Id
                    }, new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/opel.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Opel Insignia eladó4").Id
                    }, new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/opel.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Opel Insignia eladó5").Id
                    }, new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/opel.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Opel Insignia eladó6").Id
                    }
                );

                context.SaveChanges();

                context.Properties.AddRange(
                    new Property(name: "Gyártás éve", valueType: PropertyValueType.Integer)
                    {
                        isImportant = true
                    },
                    new Property(name: "Üzemanyag", valueType: PropertyValueType.SelectableFromList)
                    {
                        isImportant = true
                    },
                    new Property(name: "Kilométeróra állása", valueType: PropertyValueType.Integer)
                    {
                        isImportant = true
                    },
                    new Property(name: "Ajtók száma", valueType: PropertyValueType.Integer)
                    {
                        isImportant = false
                    },
                    new Property(name: "ABS", valueType: PropertyValueType.Bool)
                    {
                        isImportant = false
                    },
                    new Property(name: "Ködlámpa", valueType: PropertyValueType.Bool)
                    {
                        isImportant = false
                    }
                );

                context.SaveChanges();

                context.CategoryProperties.AddRange(
                    new CategoryProperty
                    {
                        CategoryId = context.Categories.Single(c => c.Name == "Személygépkocsi").Id,
                        PropertyId = context.Properties.Single(p => p.Name == "Gyártás éve").Id
                    },
                    new CategoryProperty
                    {
                        CategoryId = context.Categories.Single(c => c.Name == "Személygépkocsi").Id,
                        PropertyId = context.Properties.Single(p => p.Name == "Üzemanyag").Id
                    },
                    new CategoryProperty
                    {
                        CategoryId = context.Categories.Single(c => c.Name == "Személygépkocsi").Id,
                        PropertyId = context.Properties.Single(p => p.Name == "Kilométeróra állása").Id
                    },
                    new CategoryProperty
                    {
                        CategoryId = context.Categories.Single(c => c.Name == "Személygépkocsi").Id,
                        PropertyId = context.Properties.Single(p => p.Name == "Ajtók száma").Id
                    },
                    new CategoryProperty
                    {
                        CategoryId = context.Categories.Single(c => c.Name == "Személygépkocsi").Id,
                        PropertyId = context.Properties.Single(p => p.Name == "ABS").Id
                    },
                    new CategoryProperty
                    {
                        CategoryId = context.Categories.Single(c => c.Name == "Személygépkocsi").Id,
                        PropertyId = context.Properties.Single(p => p.Name == "Ködlámpa").Id
                    }
                );

                context.SaveChanges();

                context.PropertyValues.AddRange(
                    new PropertyValue(value: "Benzin")
                    {
                        PropertyID = context.Properties.Single(p => p.Name == "Üzemanyag").Id
                    },
                    new PropertyValue(value: "Dízel")
                    {
                        PropertyID = context.Properties.Single(p => p.Name == "Üzemanyag").Id
                    },
                    new PropertyValue(value: "LPG")
                    {
                        PropertyID = context.Properties.Single(p => p.Name == "Üzemanyag").Id
                    },
                    new PropertyValue(value: "Elektromos")
                    {
                        PropertyID = context.Properties.Single(p => p.Name == "Üzemanyag").Id
                    }
                );

                context.SaveChanges();

                context.AdvertisementProperties.AddRange(
                    new AdvertisementProperty(value: "2012")
                    {
                        AdvertisementId = context.Advertisements.Single(a => a.Title == "Audi A6").Id,
                        PropertyId = context.Properties.Single(p => p.Name == "Gyártás éve").Id
                    },
                    new AdvertisementProperty(value: "Dízel")
                    {
                        AdvertisementId = context.Advertisements.Single(a => a.Title == "Audi A6").Id,
                        PropertyId = context.Properties.Single(p => p.Name == "Üzemanyag").Id
                    },
                    new AdvertisementProperty(value: "144500")
                    {
                        AdvertisementId = context.Advertisements.Single(a => a.Title == "Audi A6").Id,
                        PropertyId = context.Properties.Single(p => p.Name == "Kilométeróra állása").Id
                    },
                    new AdvertisementProperty(value: "4")
                    {
                        AdvertisementId = context.Advertisements.Single(a => a.Title == "Audi A6").Id,
                        PropertyId = context.Properties.Single(p => p.Name == "Ajtók száma").Id
                    },
                    new AdvertisementProperty(value: "van")
                    {
                        AdvertisementId = context.Advertisements.Single(a => a.Title == "Audi A6").Id,
                        PropertyId = context.Properties.Single(p => p.Name == "ABS").Id
                    },
                    new AdvertisementProperty(value: "van")
                    {
                        AdvertisementId = context.Advertisements.Single(a => a.Title == "Audi A6").Id,
                        PropertyId = context.Properties.Single(p => p.Name == "Ködlámpa").Id
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
