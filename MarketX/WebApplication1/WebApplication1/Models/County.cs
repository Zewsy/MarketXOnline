﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Models
{
    public class County
    {
        public County(string name)
        {
            Name = name;
            Cities = new List<City>();
            UsersLiveInTheCounty = new List<User>();
        }
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<User> UsersLiveInTheCounty { get; set; }
    }
}
