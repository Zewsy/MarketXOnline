using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

        [Required(ErrorMessage = "A régi jelszó megadása kötelező!")]
        [DisplayName("Régi jelszó")]
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }

        [DisplayName("Új jelszó")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DisplayName("Új jelszó megerősítése")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }

        public int? CountyId { get; set; }
        public int? CityId { get; set; }

        [DisplayName("Telefonszám")]
        public string? PhoneNumber { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public string? OriginalProfilePicture { get; set; }
    }
}
