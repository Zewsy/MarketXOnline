using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Models
{
    public class SavedAdvertisementsUsers
    {
        public int UserID { get; set; }
        public int AdvertisementID { get; set; }

        public User User { get; set; }
        public Advertisement Advertisement { get; set; }
    }
}
