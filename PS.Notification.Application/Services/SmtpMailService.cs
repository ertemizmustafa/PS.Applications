using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using PS.Notification.Application.Abstract;
using PS.Notification.Application.Settings;
using PS.Notification.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace PS.Notification.Application.Services
{
    public class SmtpMailService : INotificationService
    {
        private readonly MailSettings _mailSettings;
        private readonly ILogger<SmtpMailService> _logger;
        public SmtpMailService(MailSettings mailSettings, ILogger<SmtpMailService> logger)
        {
            _mailSettings = mailSettings;
            _logger = logger;
        }

        public async Task SendAsync(MsgMail msgMail)
        {
            var email = new MimeMessage
            {
                Subject = msgMail.Subject,
                Body = new BodyBuilder
                {
                    HtmlBody = msgMail.Body
                }.ToMessageBody()
            };

            email.From.Add(new MailboxAddress(msgMail.FromDisplayName, msgMail.From));
            msgMail.To?.Split(",")?.ToList().ForEach(item => email.To.Add(MailboxAddress.Parse(item)));
            msgMail.Cc?.Split(",")?.ToList().ForEach(item => email.Cc.Add(MailboxAddress.Parse(item)));

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port);
            smtp.Authenticate(_mailSettings.UserName, _mailSettings.Password);
            var res = await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
