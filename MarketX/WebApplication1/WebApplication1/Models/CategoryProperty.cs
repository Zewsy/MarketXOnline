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
        public Category Category { get; set; }
        public Property Property { get; set; }
    }
}
