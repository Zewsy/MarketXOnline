using MarketX.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [DisplayName("Telefonszám")]
        public string? PhoneNumber { get; set; }

        [Required]
        [DisplayName("Regisztráció ideje")]
        public DateTime RegistrationDate { get; set; }

        public string? ProfilePicturePath { get; set; }

        public int? CountyID { get; set; }
        public int? CityID { get; set; }
        public virtual County? County { get; set; }
        public virtual City? City { get; set; }
        public virtual ICollection<WrongAdvertisementMark> WrongAdvertisementMarks { get; set; }
        public virtual ICollection<Rating> GivenRatings { get; set; }
        public virtual ICollection<MessageHeader> SentMessages { get; set; }
        public virtual ICollection<MessageHeader> ReceivedMessages { get; set; }
        public virtual ICollection<MessageHeader> OwnMessages { get; set; }
        public virtual ICollection<SavedAdvertisementsUsers> SavedAdvertisementsUsers { get; set; }
        public virtual ICollection<Advertisement> SellingAdvertisements { get; set; }
        public virtual ICollection<Advertisement> BuyingAdvertisements { get; set; }
    }
}
