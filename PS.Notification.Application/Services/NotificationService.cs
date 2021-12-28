using PS.Notification.Application.Abstract;
using System.Threading.Tasks;

namespace PS.Notification.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMailService _mailService;
        public NotificationService(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task SendEmailAsync(int mailId)
        {
            var result = await _mailService.GetMailAsync(mailId);
            await _mailService.SendSmtpMailAsync(result.Data);
        }
    }
}
