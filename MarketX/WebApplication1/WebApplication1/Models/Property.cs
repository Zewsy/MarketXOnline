using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Models
{
    public enum PropertyValueType
    {
        String,
        Integer,
        SelectableFromList,
        Bool
    }
    public class Property
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public PropertyValueType ValueType { get; set; }
        public bool isImportant { get; set; }

        public virtual ICollection<AdvertisementProperty> AdvertisementProperties { get; set; }
        public virtual ICollection<CategoryProperty> CategoryProperties { get; set; }
        public virtual ICollection<PropertyValue> PropertyValues { get; set; }
    }
}
