using MassTransit;
using Microsoft.Extensions.Logging;
using PS.Notification.Application.Abstract;
using PS.Notification.Application.Events;
using System;
using System.Threading.Tasks;

namespace PS.Notification.Application.Consumers
{
    public class MailCreatedConsumer : IConsumer<IMailCreatedEvent>
    {
        private readonly ILogger<MailCreatedConsumer> _logger;
        private readonly INotificationService _notificationService;

        public MailCreatedConsumer(ILogger<MailCreatedConsumer> logger, INotificationService notificationService)
        {
            _logger = logger;
            _notificationService = notificationService;

        }

        public async Task Consume(ConsumeContext<IMailCreatedEvent> context)
        {
            _logger.LogInformation("Value: {Message}", context.Message);
            await _notificationService.SendEmailAsync(context.Message.MailId);
          //  throw new Exception("Send to fault pls");
            await context.Publish<IMailSentEvent>(new { context.Message.CorrelationId, context.Message.MailId, SentTime = DateTime.Now });
        }
    }
}
