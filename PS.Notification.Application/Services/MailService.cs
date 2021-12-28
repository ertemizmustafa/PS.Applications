using AutoMapper;
using MailKit.Net.Smtp;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using PS.Core.Concrete;
using PS.Notification.Application.Abstract;
using PS.Notification.Application.Dtos;
using PS.Notification.Application.Events;
using PS.Notification.Application.Settings;
using PS.Notification.Domain.Entities;
using PS.Notification.Infrastructure.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PS.Notification.Application.Services
{
    public class MailService : IMailService
    {
        private readonly NotificationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly MailSettings _mailSettings;
        private readonly IPublishEndpoint _publishEndpoint;


        public MailService(NotificationDbContext dbContext, IMapper mapper, MailSettings mailSettings, IPublishEndpoint publishEndpoint)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _mailSettings = mailSettings;
        }

        public async Task<Result<int>> CreateMailAsync(CreateMailRequest mailRequest)
        {
            var entity = _mapper.Map<MsgMail>(mailRequest);
            await _dbContext.MsgMails.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            await _publishEndpoint.Publish<IMailCreatedEvent>(new { CorrelationId = Guid.NewGuid(), MailId = entity.Id });
            return Result<int>.Success(entity.Id);
        }

        public async Task<Result<int>> UpdateMailSentInfoAsync(int id, bool isSend, DateTime sentTime, string errorMessage = "")
        {
            var result = await GetMailAsync(id);
            result.Data.IsSent = isSend;
            result.Data.SendTime = sentTime;
            result.Data.ErrorMessage = errorMessage;
            _dbContext.Update(result.Data);
            return Result<int>.Success(await _dbContext.SaveChangesAsync());
        }

        public async Task<Result<MsgMail>> GetMailAsync(int id)
        {
            return Result<MsgMail>.Success(await _dbContext.MsgMails.Include(p => p.MailAttachments).FirstOrDefaultAsync(x => x.Id == id));
        }

        public async Task<Result<MsgMail>> GetMailAsync(string externalId)
        {
            return Result<MsgMail>.Success(await _dbContext.MsgMails.Include(p => p.MailAttachments).FirstOrDefaultAsync(x => x.ExternalId == externalId));
        }

        public async Task SendSmtpMailAsync(MsgMail msgMail)
        {
            var email = new MimeMessage
            {
                Subject = msgMail.Subject,
                Body = new BodyBuilder
                {
                    HtmlBody = msgMail.Body
                }.ToMessageBody()
            };

            email.From.Add(new MailboxAddress(msgMail.FromDisplayName, msgMail.From));
            msgMail.To?.Split(",")?.ToList().ForEach(item => email.To.Add(MailboxAddress.Parse(item)));
            msgMail.Cc?.Split(",")?.ToList().ForEach(item => email.Cc.Add(MailboxAddress.Parse(item)));

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port);
            smtp.Authenticate(_mailSettings.UserName, _mailSettings.Password);
            var res = await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
