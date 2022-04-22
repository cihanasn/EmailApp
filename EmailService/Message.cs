using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace EmailService
{
    public class Message
    {
        public Message(string[] to)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(o => new MailboxAddress(o)));
        }

        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
