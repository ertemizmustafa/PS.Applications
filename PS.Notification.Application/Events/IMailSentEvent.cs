using System;
namespace PS.Notification.Application.Events
{
    public interface IMailSentEvent
    {
        int MailId { get; }
        Guid CorrelationId { get; }
        DateTime SentTime { get; }
    }
}
