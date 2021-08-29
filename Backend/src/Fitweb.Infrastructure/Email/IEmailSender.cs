using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Infrastructure.Email
{
    public interface IEmailSender
    {
        Task SendIdentityEmailAsync(EmailMessage mailMessage);
    }
}
