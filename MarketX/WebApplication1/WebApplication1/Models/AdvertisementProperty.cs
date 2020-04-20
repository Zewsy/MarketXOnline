using MarketX.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Models
{
    public class AdvertisementProperty
    {
        public AdvertisementProperty(string valueAsString)
        {
            ValueAsString = valueAsString;
        }
        public int ID { get; set; }
        public int AdvertisementID { get; set; }
        public int PropertyID { get; set; }
        public virtual Advertisement Advertisement { get; set; } = null!;
        public virtual Property Property { get; set; } = null!;
        public string ValueAsString { get; set; }

        public PropertyInputField ToPropertyInputField()
        {
            return new PropertyInputField
            {
                Name = this.Property.Name,
                Value = this.ValueAsString
            };
        }

        public static List<PropertyInputField> ConvertToPropertyInputFieldList(IEnumerable<AdvertisementProperty> list)
        {
            List<PropertyInputField> results = new List<PropertyInputField>();
            foreach (var ap in list)
            {
                results.Add(ap.ToPropertyInputField());
            }
            return results;
        }
    }
}
