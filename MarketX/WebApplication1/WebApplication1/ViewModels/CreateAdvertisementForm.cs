using MarketX.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.ViewModels
{
    public class CreateAdvertisementForm
    {
        public CreateAdvertisementForm()
        {
            PropertyInputs = new List<PropertyInputField>();
            Images = new List<IFormFile>();
        }

        [Required(ErrorMessage = "Kötelező")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Kötelező")]
        public string? Category { get; set; }

        [Required(ErrorMessage = "Kötelező")]
        public bool? IsBuying { get; set; }
        public int? Price { get; set; }
        public List<PropertyInputField> PropertyInputs { get; set; }

        [Required(ErrorMessage = "Kötelező")]
        public string? County { get; set; }

        [Required(ErrorMessage = "Kötelező")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Kötelező")]
        public bool? IsUsed { get; set; }

        [Required(ErrorMessage = "Kötelező")]
        [MinLength(20, ErrorMessage = "Minimum 20 karakter hosszú")]
        public string? Description { get; set; }

        [Required]
        public int? DaysToLive { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
