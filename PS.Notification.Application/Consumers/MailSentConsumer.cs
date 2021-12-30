using MassTransit;
using Microsoft.Extensions.Logging;
using PS.Notification.Application.Abstract;
using PS.Notification.Application.Events;
using System.Threading.Tasks;

namespace PS.Notification.Application.Consumers
{

    public class MailSentConsumer : IConsumer<IMailSentEvent>
    {
        private readonly ILogger<MailSentConsumer> _logger;
        private readonly IMailService _mailService;

        public MailSentConsumer(ILogger<MailSentConsumer> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;

        }

        public async Task Consume(ConsumeContext<IMailSentEvent> context)
        {
            _logger.LogInformation($"Consumer: {nameof(MailSentConsumer)}, AttempCount: {context.GetRetryCount()}, Message: {context.Message}, CorrelationId: {context.CorrelationId}");
            _logger.LogInformation($"Updating database with mail sent success information.. MailId: {context.Message.MailId}");
            var result = await _mailService.UpdateMailSentInfoAsync(context.Message.MailId, true, context.Message.SentTime);
            _logger.LogInformation($"Updated sent success information.. Result: {result}");
        }
    }
}
