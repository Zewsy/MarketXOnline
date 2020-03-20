using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Models
{
    public class PropertyValue
    {
        public int ID { get; set; }
        public int PropertyID { get; set; }
        public virtual Property Property { get; set; }
        public string Value { get; set; }
    }
}
