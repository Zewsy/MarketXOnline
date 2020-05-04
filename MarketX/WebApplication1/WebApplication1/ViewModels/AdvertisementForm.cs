using MarketX.BLL.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.ViewModels
{
    public class AdvertisementForm
    {
        public AdvertisementForm()
        {
            PropertyInputs = new List<PropertyWithValue>();
            Images = new List<IFormFile>();
            OriginalImagePaths = new List<string>();
        }

        [Required(ErrorMessage = "Kötelező")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Kötelező")]
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Kötelező")]
        public bool? IsBuying { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        [Range(0, int.MaxValue, ErrorMessage = "Negatív érték nem adható meg árnak!")]
        public int? Price { get; set; }
        public List<PropertyWithValue> PropertyInputs { get; set; }

        [Required(ErrorMessage = "Kötelező")]
        public int? CityId { get; set; }

        [Required(ErrorMessage = "Kötelező")]
        public bool? IsUsed { get; set; }

        [Required(ErrorMessage = "Kötelező")]
        [MinLength(20, ErrorMessage = "Minimum 20 karakter hosszú")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Kötelező")]
        public int? DaysToLive { get; set; }

        [MaxLength(10, ErrorMessage = "Maximum 10 elemet válassz ki feltöltésre!")]
        public List<IFormFile> Images { get; set; }

        public List<string> OriginalImagePaths { get; set; }
    }
}
