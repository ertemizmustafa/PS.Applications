using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PS.Notification.Abstractions;
using PS.Notification.Application.Abstract;
using PS.Notification.Application.Events;
using System;
using System.Threading.Tasks;

namespace PS.Notification.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiExplorerSettings(GroupName = "default")]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;
        private readonly IPublishEndpoint _publishEndpoint;

        public MailController(IMailService mailService, IPublishEndpoint publishEndpoint)
        {
            _mailService = mailService;
            _publishEndpoint = publishEndpoint;
        }

        [ApiExplorerSettings(GroupName = "none")]
        [ProducesResponseType(200)]
        [HttpGet]
        public async Task<ActionResult> GetStatus()
        {
            return Ok(await Task.FromResult("Running.."));
        }

        [HttpPost("Create")]
        [ProducesResponseType(202)]
        [ProducesResponseType(409)]
        public async Task<ActionResult> CreateMail([FromBody] CreateMailCommand createMailCommand)
        {
            var result = await _mailService.CreateMailAsync(createMailCommand);
            if (result == 0)
            {
                return Conflict(new { message = $"Cannot create mail command.." });
            }
            else
            {
                //request saved to database and going to queue now.. Returning accepted result.
                await _publishEndpoint.Publish<IMailCreatedEvent>(new { CorrelationId = Guid.NewGuid(), MailId = result, MailCommand = createMailCommand });
                return Accepted($"MailSentInformations/{result}");
            }
        }

        [HttpGet("SentInformation/{mailId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetMailSentInformation(int mailId)
        {
            var result = await _mailService.GetMailSentInformationAsync(mailId);
            if (result == null)
            {
                return NotFound(mailId);
            }
            else
            {
                return Ok(await _mailService.GetMailSentInformationAsync(mailId));
            }
        }
    }
}
