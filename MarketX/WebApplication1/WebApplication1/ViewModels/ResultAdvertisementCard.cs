
using MarketX.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.ViewModels
{
        public class ResultAdvertisementCard
        {
            public int ID { get; set; }
            public string? Title { get; set; }

            [DisplayFormat(DataFormatString = "{0:C0}")]
            public int? Price { get; set; }

            public bool IsPriorized { get; set; }

            public string? UserName { get; set; }

            public string? ImagePath { get; set; }
            public string? County { get; set; }
            public string? City { get; set; }
            public AdType AdType { get; set; }

            public Status? Status { get; set; }

            [Display(Name = "Állapot")]
            public DAL.Entities.Condition Condition { get; set; }
        }
}
