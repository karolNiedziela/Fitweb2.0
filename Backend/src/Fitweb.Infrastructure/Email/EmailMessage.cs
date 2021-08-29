using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Email
{
    public class EmailMessage
    {
        public List<MailboxAddress> To { get; set; }

        public string Subject { get; set; }

        public string Link { get; set; }

        public string LinkText { get; set; }

        public IFormFileCollection Attachments { get; set; }

        public EmailMessage(List<string> to, string subject, string link, string linkText, IFormFileCollection attachments)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => MailboxAddress.Parse(x)));
            Subject = subject;
            Link = link;
            LinkText = linkText;
            Attachments = attachments;
        }
    }
}
