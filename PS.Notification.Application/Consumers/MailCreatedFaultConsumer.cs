using MassTransit;
using Microsoft.Extensions.Logging;
using PS.Notification.Application.Abstract;
using PS.Notification.Application.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PS.Notification.Application.Consumers
{
    public class MailCreatedFaultConsumer : IConsumer<Fault<IMailCreatedEvent>>
    {

        private readonly ILogger<MailSentConsumer> _logger;
        private readonly IMailService _mailService;

        public MailCreatedFaultConsumer(ILogger<MailSentConsumer> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;

        }

        public async Task Consume(ConsumeContext<Fault<IMailCreatedEvent>> context)
        {
            _logger.LogInformation($"Consumer: {nameof(MailCreatedFaultConsumer)}, AttempCount: {context.GetRetryCount()}, Message: {context.Message}, CorrelationId: {context.CorrelationId}");
            _logger.LogInformation($"Updating database for mail sent error information..");
            var result = await _mailService.UpdateMailSentInfoAsync(context.Message.Message.MailId, false, DateTime.Now, string.Join(",", context.Message.Exceptions?.Select(x => x.Message).ToArray()));
            _logger.LogInformation($"Updated sent error information.. Result: {result}");
        }
    }
}
