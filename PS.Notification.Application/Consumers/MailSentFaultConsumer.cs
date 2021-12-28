using MassTransit;
using Microsoft.Extensions.Logging;
using PS.Notification.Application.Abstract;
using PS.Notification.Application.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PS.Notification.Application.Consumers
{
    public class MailSentFaultConsumer : IConsumer<Fault<IMailCreatedEvent>>
    {

        private readonly ILogger<MailSentConsumer> _logger;
        private readonly IMailService _mailService;

        public MailSentFaultConsumer(ILogger<MailSentConsumer> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;

        }

        public async Task Consume(ConsumeContext<Fault<IMailCreatedEvent>> context)
        {
            _logger.LogInformation("Value: {Message}", context.Message);
            await _mailService.UpdateMailSentInfoAsync(context.Message.Message.MailId, false, DateTime.Now, string.Join(",", context.Message.Exceptions?.Select(x => x.Message).ToArray()));
        }
    }
}
