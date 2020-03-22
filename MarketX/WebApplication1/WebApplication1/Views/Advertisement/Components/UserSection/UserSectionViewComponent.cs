using MarketX.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Views.Advertisement.Components
{
    public class UserSectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(User user)
        {
            return View("UserSection", user);
        }
    }
}
