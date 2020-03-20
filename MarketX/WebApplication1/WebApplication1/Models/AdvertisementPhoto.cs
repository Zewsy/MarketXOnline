using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Models
{
    public class AdvertisementPhoto
    {
        
        public int ID { get; set; }
        public string ImagePath { get; set; }

        public int AdvertisementID { get; set; }
        public virtual Advertisement Advertisement { get; set; }
    }
}
