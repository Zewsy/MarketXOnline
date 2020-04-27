using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MarketX.DAL.Entities
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
                             Condition condition, Status status, int categoryId, int cityId)
        {
            Title = title;
            IsPriorized = isPriorized;
            CreatedDate = createdDate;
            DaysToLive = daysToLive;
            Description = description;
            Condition = condition;
            Status = status;
            CategoryId = categoryId;
            CityId = cityId;

            AdvertisementPhotos = new List<AdvertisementPhoto>();
            AdvertisementProperties = new List<AdvertisementProperty>();
            SavedAdvertisementsUsers = new List<SavedAdvertisementsUsers>();
            WrongAdvertisementMarks = new List<WrongAdvertisementMark>();
            Ratings = new List<Rating>();
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public int? Price { get; set; }
        public bool IsPriorized { get; set; }

        public DateTime CreatedDate { get; set; }
        
        [Required]
        public int DaysToLive { get; set; }
        
        public DateTime? ClosedAtDate { get; set; }
        public string Description { get; set; }
        public Condition Condition { get; set; }

        public Status Status { get; set; }
        public virtual ICollection<AdvertisementPhoto> AdvertisementPhotos { get; set; }
        public virtual ICollection<AdvertisementProperty> AdvertisementProperties { get; set; }
        public virtual ICollection<SavedAdvertisementsUsers> SavedAdvertisementsUsers { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
        
        public int CityId { get; set; }
        public virtual City City { get; set; } = null!;

        public virtual User Seller { get; set; } = null!;
        public virtual User Customer { get; set; } = null!;
        public int? SellerId { get; set; }
        public int? CustomerId { get; set; }

        public virtual ICollection<WrongAdvertisementMark> WrongAdvertisementMarks { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
