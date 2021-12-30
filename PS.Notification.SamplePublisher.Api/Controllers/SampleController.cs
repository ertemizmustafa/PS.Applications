using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PS.Notification.Abstractions;
using System.Threading.Tasks;

namespace PS.Notification.SamplePublisher.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {

        private readonly ILogger<SampleController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public SampleController(ILogger<SampleController> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMail()
        {
            await _publishEndpoint.Publish(new CreateMailCommand
            {
                ApplicationName = "SamplePublisher",
                Body = "test",
                CcRecipients = new MailContact[] { new MailContact { MailAddress = "ertemiz.mustafa@gmail.com", DisplayName = "Mustafa Ertemiz" } },
                ToRecipients = new MailContact[] { new MailContact { MailAddress = "ertemiz.mustafa@gmail.com", DisplayName = "Mustafa Ertemiz" } },
                From = new MailContact {  MailAddress = "mustafa.ertemiz@gmail.com", DisplayName = "Mustafa Ertemiz"},
                Subject = "test - sample publisher"
            });

            return Ok();
        }
    }
}
