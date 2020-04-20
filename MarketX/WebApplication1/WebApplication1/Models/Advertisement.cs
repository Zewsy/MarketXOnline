using MarketX.ViewModels;
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
        public Advertisement(string title, bool isPriorized, DateTime createdDate, int daysToLive, string description,
                             Condition condition, Status status, int categoryID, int cityID)
        {
            Title = title;
            IsPriorized = isPriorized;
            CreatedDate = createdDate;
            DaysToLive = daysToLive;
            Description = description;
            Condition = condition;
            Status = status;
            CategoryID = categoryID;
            CityID = cityID;

            AdvertisementPhotos = new List<AdvertisementPhoto>();
            AdvertisementProperties = new List<AdvertisementProperty>();
            SavedAdvertisementsUsers = new List<SavedAdvertisementsUsers>();
            WrongAdvertisementMarks = new List<WrongAdvertisementMark>();
            Ratings = new List<Rating>();
        }
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        [DisplayName("Ár")]
        [DisplayFormat(DataFormatString ="{0:C0}")]
        public int? Price { get; set; }
        public bool IsPriorized { get; set; }

        [Required]
        [DisplayName("Hirdetésfeladás dátuma")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime CreatedDate { get; set; }
        
        [Required]
        public int DaysToLive { get; set; }
        
        public DateTime? ClosedAtDate { get; set; }

        [Required]
        [MinLength(20)]
        [DisplayName("Leírás")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Állapot")]
        public Condition Condition { get; set; }

        [Required]
        public Status Status { get; set; }
        public virtual ICollection<AdvertisementPhoto> AdvertisementPhotos { get; set; }
        public virtual ICollection<AdvertisementProperty> AdvertisementProperties { get; set; }
        public virtual ICollection<SavedAdvertisementsUsers> SavedAdvertisementsUsers { get; set; }

        [Required]
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; } = null!;
        
        [Required]
        public int CityID { get; set; }
        public virtual City City { get; set; } = null!;

        public virtual User Seller { get; set; } = null!;
        public virtual User Customer { get; set; } = null!;
        public int? SellerID { get; set; }
        public int? CustomerID { get; set; }

        public virtual ICollection<WrongAdvertisementMark> WrongAdvertisementMarks { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }


        public static IEnumerable<Advertisement> FilterAdvertisementsByProperties(IEnumerable<Advertisement> advertisements, List<PropertyInputField> propertyInputs)
        {
            return advertisements.Where(a => propertyInputs.All(pi =>
                                        a.AdvertisementProperties.Any(ap => ap.Property.Name == pi.Name && pi.Value == ap.ValueAsString)));
        }
    }
}
