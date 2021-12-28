using Microsoft.AspNetCore.Mvc;
using PS.Notification.Application.Abstract;
using PS.Notification.Application.Dtos;
using System.Threading.Tasks;

namespace PS.Notification.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class MailController : ControllerBase
    {
        private readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateMail(CreateMailRequest createMailRequest)
        {
            return Ok(await _mailService.CreateMailAsync(createMailRequest));
        }

        [HttpGet]
        public async Task<ActionResult> GetMail(string externalId)
        {
            return Ok(await _mailService.GetMailAsync(externalId));
        }
    }
}
