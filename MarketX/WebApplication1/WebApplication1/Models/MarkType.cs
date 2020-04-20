using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Models
{
    public class MarkType
    {
        public MarkType(string name)
        {
            Name = name;
            WrongAdvertisementMarks = new List<WrongAdvertisementMark>();
        }
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<WrongAdvertisementMark> WrongAdvertisementMarks { get; set; }
    }
}
