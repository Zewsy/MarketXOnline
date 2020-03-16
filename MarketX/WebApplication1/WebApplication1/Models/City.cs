using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketX.Models;

namespace MarketX.Models
{
    public class City
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CountyID { get; set; }
        public County County { get; set; }
        public ICollection<User> UsersLiveInTheCity { get; set; }
        public ICollection<Advertisement> Advertisements { get; set; }
    }
}
