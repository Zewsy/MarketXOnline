using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Models
{
    public class Rating
    {
        public int ID { get; set; }
        public virtual Advertisement Advertisement { get; set; }
        public string? Description { get; set; }  
    }
}
