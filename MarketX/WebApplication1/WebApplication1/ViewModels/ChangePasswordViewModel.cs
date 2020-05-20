using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MarketX.ViewModels
{
    public class ChangePasswordViewModel
    {
        public int Id { get; set; }

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

    }
}
