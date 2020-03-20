using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Models
{
    public class MessageHeader
    {
        public int ID { get; set; }
        public bool IsRead { get; set; }
        public DateTime DeleteDate { get; set; }
        public virtual User Owner { get; set; }
        public int? OwnerID { get; set; }
        public int? SenderID { get; set; }
        public int RecipientID { get; set; }
        public virtual User? Sender { get; set; }
        public virtual User? Recipient { get; set; }
        public virtual MessageContent MessageContent { get; set; }
    }
}
