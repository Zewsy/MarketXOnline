using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Models
{
    public class AdvertisementProperty
    {
        public int ID { get; set; }
        public int AdvertisementID { get; set; }
        public int PropertyID { get; set; }
        public Advertisement Advertisement { get; set; }
        public Property Property { get; set; }
        public string ValueAsString { get; set; }
    }
}
