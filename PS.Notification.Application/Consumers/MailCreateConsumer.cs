using MassTransit;
using Microsoft.Extensions.Logging;
using PS.Notification.Abstractions;
using PS.Notification.Application.Abstract;
using PS.Notification.Application.Events;
using System.Threading.Tasks;

namespace PS.Notification.Application.Consumers
{
    public class MailCreateConsumer : IConsumer<CreateMailCommand>
    {
        private readonly ILogger<MailCreateConsumer> _logger;
        private readonly IMailService _mailService;

        public MailCreateConsumer(ILogger<MailCreateConsumer> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        public async Task Consume(ConsumeContext<CreateMailCommand> context)
        {
            _logger.LogInformation($"Consumer: {nameof(MailCreatedConsumer)}, AttempCount: {context.GetRetryCount()}, Message: {context.Message}, CorrelationId: {context.CorrelationId}");
            _logger.LogInformation($"Creating mail information on database.. ");
            var result = await _mailService.CreateMailAsync(context.Message);
            _logger.LogInformation($"Mail information created successfully on database.. MailId: {result}");
            _logger.LogInformation($"Publishing {nameof(IMailCreatedEvent)}..");
            await context.Publish<IMailCreatedEvent>(new { MailId = result, context.CorrelationId, MailCommand = context.Message });
            _logger.LogInformation($"Published {nameof(IMailCreatedEvent)}..");
        }
    }
}
