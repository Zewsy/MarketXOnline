using System;
using System.Collections.Generic;
using System.Text;

namespace MarketX.BLL.DTOs
{
    public class City
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public County? County { get; set; }
    }
}
