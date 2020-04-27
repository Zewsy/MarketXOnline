using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MarketX.BLL.DTOs
{
    public class LoginUser
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Jelszó")]
        public string? Password { get; set; }

        [Display(Name = "Emlékezz rám")]
        public bool RememberMe { get; set; }
    }
}
