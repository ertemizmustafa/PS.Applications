using PS.Notification.Abstractions;
using PS.Notification.Application.Dtos;
using System;
using System.Threading.Tasks;

namespace PS.Notification.Application.Abstract
{
    public interface IMailService
    {
        Task<int> CreateMailAsync(CreateMailCommand sendMailCommand);
        Task<bool> UpdateMailSentInfoAsync(int id, bool isSend, DateTime sentTime, string errorMessage = "");
        Task<MailSentInfoResposeDto> GetMailSentInformationAsync(int id);
        Task SendSmtpMailAsync(CreateMailCommand mailCommand);
    }
}
