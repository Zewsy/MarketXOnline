using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.DAL.Entities
{
    public class MessageHeader
    {
        public MessageHeader()
        {
            IsRead = false;
        }

        public int ID { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DeleteDate { get; set; }
        public virtual User Owner { get; set; } = null!;
        public int? OwnerID { get; set; }
        public int? SenderID { get; set; }
        public int RecipientID { get; set; }
        public virtual User Sender { get; set; } = null!;
        public virtual User Recipient { get; set; } = null!;
        public virtual MessageContent MessageContent { get; set; } = null!;
    }
}
