using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.DAL.Entities
{
    public class CategoryProperty
    {
        public int CategoryId { get; set; }
        public int PropertyId { get; set; }
        public virtual Category Category { get; set; } = null!;
        public virtual Property Property { get; set; } = null!;
    }
}
