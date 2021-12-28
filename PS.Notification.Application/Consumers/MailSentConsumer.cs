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
            _logger.LogInformation("Value: {Message}", context.Message);
            await _mailService.UpdateMailSentInfoAsync(context.Message.MailId, true, context.Message.SentTime);
        }
    }
}
