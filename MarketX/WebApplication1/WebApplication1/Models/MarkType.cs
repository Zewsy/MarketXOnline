using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Models
{
    public class MarkType
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<WrongAdvertisementMark> WrongAdvertisementMarks { get; set; }
    }
}
