using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketX.BLL.DTOs
{
    public class Property
    {
        public Property()
        {
            PropertyValues = new List<PropertyValue>();
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsImportant { get; set; }
        public DAL.Entities.PropertyValueType ValueType { get; set; }
        public List<PropertyValue> PropertyValues { get; set; }
    }
}
