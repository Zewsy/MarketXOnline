using MarketX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.ViewModels
{
    public class PropertiesWithInputs
    {
        public PropertiesWithInputs()
        {
            PropertyInputs = new List<PropertyInputField>();
            Properties = new List<Property>();
        }
        public List<PropertyInputField> PropertyInputs { get; set; }
        public List<Property> Properties { get; set; }
    }
}
