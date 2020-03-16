using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Models
{
    public class WrongAdvertisementMark
    {
        public int ID { get; set; }
        public User MarkingUser { get; set; }
        public Advertisement Advertisement { get; set; }
        public string? Note { get; set; }
        public MarkType MarkType { get; set; }
    }
}
