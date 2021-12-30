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
        private readonly IMailService _mailService;

        public MailCreatedConsumer(ILogger<MailCreatedConsumer> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        public async Task Consume(ConsumeContext<IMailCreatedEvent> context)
        {
            _logger.LogInformation($"Consumer: {nameof(MailCreatedConsumer)}, AttempCount: {context.GetRetryCount()}, Message: {context.Message}, CorrelationId: {context.CorrelationId}");
            _logger.LogInformation($"Sending mail with smtp client...");
            await _mailService.SendSmtpMailAsync(context.Message.MailCommand);
            _logger.LogInformation($"Successfully sent mail with smtp client...");
            _logger.LogInformation($"Publishing {nameof(IMailSentEvent)}..");
            await context.Publish<IMailSentEvent>(new { context.Message.CorrelationId, context.Message.MailId, SentTime = DateTime.Now });
            _logger.LogInformation($"Published {nameof(IMailSentEvent)}..");
        }
    }
}
