using MarketX.DAL;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.DAL.Entities
{
    public class User : IdentityUser<int>
    {
        public User(string firstName, string lastName, string email, DateTime registrationDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            RegistrationDate = registrationDate;
            WrongAdvertisementMarks = new List<WrongAdvertisementMark>();
            GivenRatings = new List<Rating>();
            SentMessages = new List<MessageHeader>();
            ReceivedMessages = new List<MessageHeader>();
            OwnMessages = new List<MessageHeader>();
            SavedAdvertisementsUsers = new List<SavedAdvertisementsUsers>();
            SellingAdvertisements = new List<Advertisement>();
            BuyingAdvertisements = new List<Advertisement>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string? ProfilePicturePath { get; set; }

        public int? CountyId { get; set; }
        public int? CityId { get; set; }
        public virtual County County { get; set; } = null!;
        public virtual City City { get; set; } = null!;
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
