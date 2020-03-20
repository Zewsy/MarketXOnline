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
        New,
        Closed
    }

    public class Advertisement
    {
        public int ID { get; set; }
        public string Title { get; set; }

        [DisplayName("Ár")]
        [DisplayFormat(DataFormatString ="{0:C0}")]
        public int? Price { get; set; }
        public bool IsPriorized { get; set; }

        [DisplayName("Hirdetésfeladás dátuma")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime CreatedDate { get; set; }
        public int DaysToLive { get; set; }
        public DateTime? ClosedAtDate { get; set; }

        [DisplayName("Leírás")]
        public string Description { get; set; }

        [DisplayName("Állapot")]
        public Condition Condition { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<AdvertisementPhoto> AdvertisementPhotos { get; set; }
        public virtual ICollection<AdvertisementProperty> AdvertisementProperties { get; set; }
        public virtual ICollection<SavedAdvertisementsUsers> SavedAdvertisementsUsers { get; set; }

        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public int CityID { get; set; }
        public virtual City City { get; set; }

        public virtual User? Seller { get; set; }
        public virtual User? Customer { get; set; }
        public int? SellerID { get; set; }
        public int? CustomerID { get; set; }

        public virtual ICollection<WrongAdvertisementMark> WrongAdvertisementMarks { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

    }
}
