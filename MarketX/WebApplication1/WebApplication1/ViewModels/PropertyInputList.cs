using MarketX.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.ViewModels
{
    public class PropertyInputList
    {
        public PropertyInputList()
        {
            PropertyInputs = new List<PropertyWithValue>();
        }
        public List<PropertyWithValue> PropertyInputs { get; set; }
    }
}
