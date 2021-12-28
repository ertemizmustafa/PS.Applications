using PS.Notification.Domain.Entities;
using System.Threading.Tasks;

namespace PS.Notification.Application.Abstract
{
    public interface INotificationService
    {
        Task SendEmailAsync(int mailId);
    }
}
