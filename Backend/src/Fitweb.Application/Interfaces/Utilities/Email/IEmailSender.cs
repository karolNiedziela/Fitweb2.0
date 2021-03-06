using Fitweb.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitweb.Application.Interfaces.Utilities.Email
{
    public interface IEmailSender
    {
        Task SendIdentityEmailAsync(EmailMessage mailMessage);
    }
}
