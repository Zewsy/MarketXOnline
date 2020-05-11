using MarketX.BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.ViewModels.Admin
{
    public class EditCategoriesViewModel
    {
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
