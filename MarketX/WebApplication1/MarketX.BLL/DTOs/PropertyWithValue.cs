using System;
using System.Collections.Generic;
using System.Text;

namespace MarketX.BLL.DTOs
{
    public class PropertyWithValue
    {
        public PropertyWithValue()
        {

        }
        public PropertyWithValue(Property property)
        {
            Property = property;
        }
        public Property Property { get; set; } = null!; //TODO
        public string? Value { get; set; }
    }
}
