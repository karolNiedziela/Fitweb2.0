using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Fitweb.Application.Interfaces.Utilities.Email;
using Fitweb.Application.Models;
using Fitweb.Application.Exceptions;

namespace Fitweb.Infrastructure.Utilities.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task SendIdentityEmailAsync(EmailMessage emailMessage)
        {
            var message = CreateIdentityEmailMessage(emailMessage);

            await SendAsync(message);         
        }

        private MimeMessage CreateIdentityEmailMessage(EmailMessage emailMessage)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(MailboxAddress.Parse(_emailSettings.Address));
            mimeMessage.To.AddRange(emailMessage.To);
            mimeMessage.Subject = emailMessage.Subject;

            var builder = new BodyBuilder
            {
                HtmlBody = string.Format("<a href={0}>{1}</a>", emailMessage.Link, emailMessage.LinkText)
            };

            if (emailMessage.Attachments is not null)
            {
                byte[] fileBytes;
                foreach (var attachment in emailMessage.Attachments)
                {
                    if (attachment.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();

                        builder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                    }
                }
            }

            mimeMessage.Body = builder.ToMessageBody();

            return mimeMessage;
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            var testSmtp = new System.Net.Mail.SmtpClient(_emailSettings.Host, _emailSettings.Port);
            testSmtp.EnableSsl = true;
            System.Net.NetworkCredential credentials = new
                System.Net.NetworkCredential(_emailSettings.Address, _emailSettings.Password);
            testSmtp.UseDefaultCredentials = false;
            testSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            testSmtp.Credentials = credentials;
            var message = new MailMessage(_emailSettings.Address, "imniedziel@gmail.com");
            try
            {
                testSmtp.Send(message);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            //using var smtp = new SmtpClient();
            //try
            //{
            //    await smtp.ConnectAsync(_emailSettings.Host, _emailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            //    await smtp.AuthenticateAsync(_emailSettings.Address, _emailSettings.Password);

            //    await smtp.SendAsync(mailMessage);
            //}
            //catch(Exception ex)
            //{
            //    throw new EmailSendingException(ex.Message);
            //}
            //finally
            //{
            //    await smtp.DisconnectAsync(true);
            //    smtp.Dispose();
            //}
        }
    }
}
