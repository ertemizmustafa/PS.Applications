using PS.Notification.Abstractions;
using System;

namespace PS.Notification.Application.Events
{
    public interface IMailCreatedEvent
    {
        int MailId { get; }
        Guid CorrelationId { get; }
        CreateMailCommand MailCommand { get; }
    }
}