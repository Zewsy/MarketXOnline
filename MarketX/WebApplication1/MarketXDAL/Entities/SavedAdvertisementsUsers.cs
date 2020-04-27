using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.DAL.Entities
{
    public class SavedAdvertisementsUsers
    {
        public int UserID { get; set; }
        public int AdvertisementID { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Advertisement Advertisement { get; set; } = null!;
    }
}
