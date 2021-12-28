using PS.Core.Concrete;
using PS.Notification.Application.Dtos;
using PS.Notification.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PS.Notification.Application.Abstract
{
    public interface IMailService
    {
        Task<Result<int>> CreateMailAsync(CreateMailRequest mailRequest);
        Task<Result<int>> UpdateMailSentInfoAsync(int id, bool isSend, DateTime sentTime, string errorMessage = "");
        Task<Result<MsgMail>> GetMailAsync(int id);
        Task<Result<MsgMail>> GetMailAsync(string externalId);
        Task SendSmtpMailAsync(MsgMail msgMail);
    }
}
