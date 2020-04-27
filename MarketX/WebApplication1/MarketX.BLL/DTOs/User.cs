using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MarketX.BLL.DTOs
{
    public class User
    {
        public User(string firstName, string lastName, string password, string email, DateTime registrationDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            RegistrationDate = registrationDate;
        }
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [DisplayName("Telefonszám")]
        public string? PhoneNumber { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        [DisplayName("Regisztráció ideje")]
        public DateTime RegistrationDate { get; set; }
        public string? ProfilePicturePath { get; set; }
        public County? County { get; set; }
        public City? City { get; set; }

    }
}
