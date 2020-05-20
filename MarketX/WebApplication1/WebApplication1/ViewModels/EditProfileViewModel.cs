using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MarketX.ViewModels
{
    public class EditProfileViewModel
    {
        public int Id { get; set; }

        [DisplayName("Keresztnév")]
        [Required]
        public string? FirstName { get; set; }

        [DisplayName("Vezetéknév")]
        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Email { get; set; }

        public int? CountyId { get; set; }
        public int? CityId { get; set; }

        [DisplayName("Telefonszám")]
        public string? PhoneNumber { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public string? OriginalProfilePicture { get; set; }
    }
}
