using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.ViewModels
{
    public class UserAdvertisementsViewModel
    {
        public UserAdvertisementsViewModel()
        {
            NewAdvertisements = new List<ResultAdvertisementCard>();
            ActiveAdvertisements = new List<ResultAdvertisementCard>();
        }
        public List<ResultAdvertisementCard> NewAdvertisements { get; set; }
        public List<ResultAdvertisementCard> ActiveAdvertisements { get; set; }
    }
}
