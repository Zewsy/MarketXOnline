using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Models
{
    public enum Condition
    {
        Used,
        New
    }

    public enum Status
    {
        Active,
        Expired,
        Blocked,
        New
    }

    public class Advertisement
    {
        public int ID { get; set; }
        public string Title { get; set; }

        [DisplayName("Ár")]
        public int? Price { get; set; }
        public bool IsPriorized { get; set; }

        [DisplayName("Hirdetésfeladás dátuma")]
        public DateTime CreatedDate { get; set; }
        public int DaysToLive { get; set; }
        public DateTime? ClosedAtDate { get; set; }

        [DisplayName("Leírás")]
        public string Description { get; set; }

        [DisplayName("Állapot")]
        public Condition Condition { get; set; }
        public Status Status { get; set; }
        public ICollection<AdvertisementPhoto> AdvertisementPhotos { get; set; }
        public ICollection<AdvertisementProperty> AdvertisementProperties { get; set; }
        public ICollection<SavedAdvertisementsUsers> SavedAdvertisementsUsers { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public int CityID { get; set; }
        public City City { get; set; }

        public User? Seller { get; set; }
        public User? Customer { get; set; }
        public int? SellerID { get; set; }
        public int? CustomerID { get; set; }

        public ICollection<WrongAdvertisementMark> WrongAdvertisementMarks { get; set; }

        public ICollection<Rating> Ratings { get; set; }

    }
}
