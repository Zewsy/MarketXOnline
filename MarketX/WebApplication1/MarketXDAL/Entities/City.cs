using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketX.DAL;

namespace MarketX.DAL.Entities
{
    public class City
    {
        public City(string name)
        {
            Name = name;
            UsersLiveInTheCity = new List<User>();
            Advertisements = new List<Advertisement>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountyId { get; set; }
        public virtual County County { get; set; } = null!;
        public virtual ICollection<User> UsersLiveInTheCity { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set; }
    }
}
