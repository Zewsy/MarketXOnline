using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Models
{
    public class County
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<City> Cities { get; set; }
        public ICollection<User> UsersLiveInTheCounty { get; set; }
    }
}
