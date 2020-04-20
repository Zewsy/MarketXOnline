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
                    new County(name: "Budapest"),
                    new County(name: "Pest megye"),
                    new County(name: "Jász-Nagykun-Szolnok megye")
                );

                context.SaveChanges();

                context.Cities.AddRange(
                    new City(name: "XI. Kerület")
                    {
                        CountyID = context.Counties.Single(c => c.Name == "Budapest").ID
                    },
                    new City(name: "Maglód")
                    {
                        CountyID = context.Counties.Single(c => c.Name == "Pest megye").ID
                    },
                    new City(name: "Jászárokszállás")
                    {
                        CountyID = context.Counties.Single(c => c.Name == "Jász-Nagykun-Szolnok megye").ID
                    }
                );

                context.SaveChanges();

                context.Users.AddRange(
                    new User
                    (
                        firstName: "János",
                        lastName: "Kovács",
                        email: "kovacsjanos@asd.hu",
                        password: "12345",
                        registrationDate: new DateTime(1990, 03, 10)
                    )
                    {
                        PhoneNumber = "+36705556666",
                        CountyID = context.Counties.Single(c => c.Name == "Jász-Nagykun-Szolnok megye").ID,
                        CityID = context.Cities.Single(c => c.Name == "Jászárokszállás").ID,
                        ProfilePicturePath = "~/images/profilePictures/KovacsJanos.jpg"
                    },
                    new User
                    (
                        firstName: "Ferenc",
                        lastName: "Kis",
                        email: "kisferenc@asd.hu",
                        password: "kisferike5",
                        registrationDate: new DateTime(1998, 07, 19)
                    )
                    {
                        PhoneNumber = "+36101112222",
                        CountyID = context.Counties.Single(c => c.Name == "Budapest").ID,
                        CityID = context.Cities.Single(c => c.Name == "XI. Kerület").ID,
                        ProfilePicturePath = "~/images/profilePictures/KisFerenc.jpg"
                    },
                    new User
                    (
                        firstName: "Mirjam",
                        lastName: "Lippai",
                        email: "andika@asd.hu",
                        password: "asd123",
                        registrationDate: new DateTime(1999, 02, 10)
                    )
                    {
                        CountyID = context.Counties.Single(c => c.Name == "Pest megye").ID,
                        CityID = context.Cities.Single(c => c.Name == "Maglód").ID
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
                        ParentCategoryID = context.Categories.Single(c => c.Name=="Ruházat").ID
                    }, 
                    new Category(name: "Személygépkocsi")
                    {
                        ParentCategoryID = context.Categories.Single(c => c.Name == "Autó, motor, alkatrész").ID
                    },
                    new Category(name: "Lakás")
                    {
                        ParentCategoryID = context.Categories.Single(c => c.Name == "Ingatlan").ID
                    },
                     new Category(name: "Laptop, notebook")
                     {
                         ParentCategoryID = context.Categories.Single(c => c.Name == "Számítástechnika").ID
                     }
                 );

                context.SaveChanges();

                context.Categories.Add(new Category(name: "Uniszex cipő")
                {
                    ParentCategoryID = context.Categories.Single(c => c.Name == "Uniszex ruházat").ID
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
                        cityID: context.Cities.Single(c => c.Name == "Maglód").ID,
                        categoryID: context.Categories.Single(c => c.Name == "Uniszex cipő").ID
                    )
                    {
                        Price = 20000,
                        SellerID = context.Users.Single(u => u.FirstName == "János").ID,
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
                        cityID: context.Cities.Single(c => c.Name == "XI. Kerület").ID,
                        categoryID: context.Categories.Single(c => c.Name == "Személygépkocsi").ID
                    )
                    {
                        Price = 5500000,
                        SellerID = context.Users.Single(u => u.FirstName == "János").ID
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
                        cityID: context.Cities.Single(c => c.Name == "XI. Kerület").ID,
                        categoryID: context.Categories.Single(c => c.Name == "Lakás").ID
                    )
                    {
                        CustomerID = context.Users.Single(u => u.FirstName == "Ferenc").ID
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
                        cityID: context.Cities.Single(c => c.Name == "XI. Kerület").ID,
                        categoryID: context.Categories.Single(c => c.Name == "Lakás").ID
                    )
                    {
                        Price = 60000000,  
                        SellerID = context.Users.Single(u => u.FirstName == "János").ID
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
                        cityID: context.Cities.Single(c => c.Name == "Jászárokszállás").ID,
                        categoryID: context.Categories.Single(c => c.Name == "Személygépkocsi").ID
                    )
                    {
                        Price = 9000000,
                        SellerID = context.Users.Single(u => u.FirstName == "Mirjam").ID
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
                        cityID: context.Cities.Single(c => c.Name == "Jászárokszállás").ID,
                        categoryID: context.Categories.Single(c => c.Name == "Személygépkocsi").ID
                    )
                    {
                        Price = 9000000,
                        SellerID = context.Users.Single(u => u.FirstName == "Mirjam").ID
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
                        cityID: context.Cities.Single(c => c.Name == "Jászárokszállás").ID,
                        categoryID: context.Categories.Single(c => c.Name == "Személygépkocsi").ID
                    )
                    {
                        Price = 9000000,
                        SellerID = context.Users.Single(u => u.FirstName == "Mirjam").ID
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
                        cityID: context.Cities.Single(c => c.Name == "Jászárokszállás").ID,
                        categoryID: context.Categories.Single(c => c.Name == "Személygépkocsi").ID
                    )
                    {
                        Price = 9000000,
                        SellerID = context.Users.Single(u => u.FirstName == "Mirjam").ID
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
                        cityID: context.Cities.Single(c => c.Name == "Jászárokszállás").ID,
                        categoryID: context.Categories.Single(c => c.Name == "Személygépkocsi").ID
                    )
                    {
                        Price = 9000000,
                        SellerID = context.Users.Single(u => u.FirstName == "Mirjam").ID
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
                        cityID: context.Cities.Single(c => c.Name == "Jászárokszállás").ID,
                        categoryID: context.Categories.Single(c => c.Name == "Személygépkocsi").ID
                    )
                    {
                        Price = 9000000,
                        SellerID = context.Users.Single(u => u.FirstName == "Mirjam").ID
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
                        cityID: context.Cities.Single(c => c.Name == "XI. Kerület").ID,
                        categoryID: context.Categories.Single(c => c.Name == "Személygépkocsi").ID
                    )
                    {
                        Price = 8000000,
                        SellerID = context.Users.Single(u => u.FirstName == "Mirjam").ID
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
                        cityID: context.Cities.Single(c => c.Name == "Maglód").ID,
                        categoryID: context.Categories.Single(c => c.Name == "Személygépkocsi").ID
                    )
                    {
                        Price = 7000000,
                        SellerID = context.Users.Single(u => u.FirstName == "Mirjam").ID
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
                        cityID: context.Cities.Single(c => c.Name == "XI. Kerület").ID,
                        categoryID: context.Categories.Single(c => c.Name == "Személygépkocsi").ID
                    )
                    {
                        Price = 4000000,
                        SellerID = context.Users.Single(u => u.FirstName == "Mirjam").ID
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
                        cityID: context.Cities.Single(c => c.Name == "Maglód").ID,
                        categoryID: context.Categories.Single(c => c.Name == "Laptop, notebook").ID
                    )
                    {                      
                        Price = 800000,
                        SellerID = context.Users.Single(u => u.FirstName == "János").ID
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
                        cityID: context.Cities.Single(c => c.Name == "XI. Kerület").ID,
                        categoryID: context.Categories.Single(c => c.Name == "Laptop, notebook").ID
                    )
                    {
                        Price = 600000,                        
                        SellerID = context.Users.Single(u => u.FirstName == "Mirjam").ID,
                    }
                );

                context.SaveChanges();

                context.AdvertisementPhotos.AddRange(
                    new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/audi.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title== "Audi A6").ID
                    },
                    new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/audi_2.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Audi A6").ID
                    },
                    new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/audi_3.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Audi A6").ID
                    },
                    new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/turabakancs.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Eladó túrabakancs").ID
                    },
                    new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/flat.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Lakás eladó").ID
                    },
                    new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/opel.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Opel Insignia eladó").ID
                    },
                    new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/acer.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Acer Predator").ID
                    },
                    new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/asus.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Asus Rog").ID
                    },
                    new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/opel.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Opel Insignia eladó2").ID
                    }, new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/opel.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Opel Insignia eladó3").ID
                    }, new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/opel.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Opel Insignia eladó4").ID
                    }, new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/opel.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Opel Insignia eladó5").ID
                    }, new AdvertisementPhoto(imagePath: "~/images/advertisementPhotos/opel.jpg")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Opel Insignia eladó6").ID
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
                        CategoryID = context.Categories.Single(c => c.Name == "Személygépkocsi").ID,
                        PropertyID = context.Properties.Single(p => p.Name == "Gyártás éve").ID
                    },
                    new CategoryProperty
                    {
                        CategoryID = context.Categories.Single(c => c.Name == "Személygépkocsi").ID,
                        PropertyID = context.Properties.Single(p => p.Name == "Üzemanyag").ID
                    },
                    new CategoryProperty
                    {
                        CategoryID = context.Categories.Single(c => c.Name == "Személygépkocsi").ID,
                        PropertyID = context.Properties.Single(p => p.Name == "Kilométeróra állása").ID
                    },
                    new CategoryProperty
                    {
                        CategoryID = context.Categories.Single(c => c.Name == "Személygépkocsi").ID,
                        PropertyID = context.Properties.Single(p => p.Name == "Ajtók száma").ID
                    },
                    new CategoryProperty
                    {
                        CategoryID = context.Categories.Single(c => c.Name == "Személygépkocsi").ID,
                        PropertyID = context.Properties.Single(p => p.Name == "ABS").ID
                    },
                    new CategoryProperty
                    {
                        CategoryID = context.Categories.Single(c => c.Name == "Személygépkocsi").ID,
                        PropertyID = context.Properties.Single(p => p.Name == "Ködlámpa").ID
                    }
                );

                context.SaveChanges();

                context.PropertyValues.AddRange(
                    new PropertyValue(value: "Benzin")
                    {
                        PropertyID = context.Properties.Single(p => p.Name == "Üzemanyag").ID
                    },
                    new PropertyValue(value: "Dízel")
                    {
                        PropertyID = context.Properties.Single(p => p.Name == "Üzemanyag").ID
                    },
                    new PropertyValue(value: "LPG")
                    {
                        PropertyID = context.Properties.Single(p => p.Name == "Üzemanyag").ID
                    },
                    new PropertyValue(value: "Elektromos")
                    {
                        PropertyID = context.Properties.Single(p => p.Name == "Üzemanyag").ID
                    }
                );

                context.SaveChanges();

                context.AdvertisementProperties.AddRange(
                    new AdvertisementProperty(valueAsString: "2012")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Audi A6").ID,
                        PropertyID = context.Properties.Single(p => p.Name == "Gyártás éve").ID
                    },
                    new AdvertisementProperty(valueAsString: "Dízel")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Audi A6").ID,
                        PropertyID = context.Properties.Single(p => p.Name == "Üzemanyag").ID
                    },
                    new AdvertisementProperty(valueAsString: "144500")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Audi A6").ID,
                        PropertyID = context.Properties.Single(p => p.Name == "Kilométeróra állása").ID
                    },
                    new AdvertisementProperty(valueAsString: "4")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Audi A6").ID,
                        PropertyID = context.Properties.Single(p => p.Name == "Ajtók száma").ID
                    },
                    new AdvertisementProperty(valueAsString: "van")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Audi A6").ID,
                        PropertyID = context.Properties.Single(p => p.Name == "ABS").ID
                    },
                    new AdvertisementProperty(valueAsString: "van")
                    {
                        AdvertisementID = context.Advertisements.Single(a => a.Title == "Audi A6").ID,
                        PropertyID = context.Properties.Single(p => p.Name == "Ködlámpa").ID
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
