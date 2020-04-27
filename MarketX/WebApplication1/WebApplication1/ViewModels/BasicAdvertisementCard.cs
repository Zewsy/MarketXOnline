using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
public enum AdType
{
    Selling,
    Buying
}
namespace MarketX.ViewModels
{
    public class BasicAdvertisementCard
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public int? Price { get; set; }
        public string? ImagePath { get; set; }
        public string? City { get; set; }
        public AdType AdType { get; set; }
    }
}
