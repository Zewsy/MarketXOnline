using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Models
{
    public class CategoryProperty
    {
        public int CategoryID { get; set; }
        public int PropertyID { get; set; }
        public virtual Category Category { get; set; } = null!;
        public virtual Property Property { get; set; } = null!;
    }
}
