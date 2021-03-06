﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.DAL.Entities
{
    public class Rating
    {
        public int ID { get; set; }
        public virtual Advertisement Advertisement { get; set; } = null!;
        public string? Description { get; set; }  
    }
}
