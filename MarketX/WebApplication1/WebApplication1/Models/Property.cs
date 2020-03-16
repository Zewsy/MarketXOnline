using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Models
{
    public class Property
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsImportant { get; set; }

        public ICollection<AdvertisementProperty> AdvertisementProperties { get; set; }
        public ICollection<CategoryProperty> CategoryProperties { get; set; }
    }
}
