using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MarketX.BLL.DTOs
{
    public class Advertisement
    {
        public Advertisement(string title, bool isPriorized, DateTime createdDate, int daysToLive, string description,
                                    DAL.Entities.Condition condition, DAL.Entities.Status status, int categoryId, int cityId)
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

            AdvertisementImagePaths = new List<string>();
            AdvertisementProperties = new List<PropertyWithValue>();
        }
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [DisplayName("Ár")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public int? Price { get; set; }
        public bool IsPriorized { get; set; }

        [Required]
        [DisplayName("Hirdetésfeladás dátuma")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime CreatedDate { get; set; }
        public int DaysToLive { get; set; }

        [Required]
        [MinLength(20)]
        [DisplayName("Leírás")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Állapot")]
        public DAL.Entities.Condition Condition { get; set; }
        
        [Required]
        public DAL.Entities.Status Status { get; set; }
        public List<string> AdvertisementImagePaths { get; set; }
        public List<PropertyWithValue> AdvertisementProperties { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int CityId { get; set; }
        public City City { get; set; } = null!;

        public int? SellerId { get; set; }
        public User? Seller { get; set; }
        public int? CustomerId { get; set; }
        public User? Customer { get; set; }
    }
}
