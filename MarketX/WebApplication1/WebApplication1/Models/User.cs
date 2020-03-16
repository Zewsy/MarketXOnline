using MarketX.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        public string? ProfilePicturePath { get; set; }

        public int? CountyID { get; set; }
        public int? CityID { get; set; }
        public County? County { get; set; }
        public City? City { get; set; }
        public ICollection<WrongAdvertisementMark> WrongAdvertisementMarks { get; set; }
        public ICollection<Rating> GivenRatings { get; set; }
        public ICollection<MessageHeader> SentMessages { get; set; }
        public ICollection<MessageHeader> ReceivedMessages { get; set; }
        public ICollection<MessageHeader> OwnMessages { get; set; }
        public ICollection<SavedAdvertisementsUsers> SavedAdvertisementsUsers { get; set; }
        public ICollection<Advertisement> SellingAdvertisements { get; set; }
        public ICollection<Advertisement> BuyingAdvertisements { get; set; }
    }
}
