using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Models
{
    public class MessageContent
    {
        public MessageContent(string title, string content)
        {
            Title = title;
            Content = content;
            MessageHeaders = new List<MessageHeader>();
        }
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<MessageHeader> MessageHeaders { get; set; }

    }
}
