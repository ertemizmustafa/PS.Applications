using AutoMapper;
using MailKit.Net.Smtp;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using PS.Notification.Abstractions;
using PS.Notification.Application.Abstract;
using PS.Notification.Application.Dtos;
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


        public MailService(NotificationDbContext dbContext, IMapper mapper, MailSettings mailSettings)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _mailSettings = mailSettings;
        }

        public async Task<int> CreateMailAsync(CreateMailCommand sendMailCommand)
        {
            var entity = _mapper.Map<MsgMail>(sendMailCommand);
            await _dbContext.MsgMails.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> UpdateMailSentInfoAsync(int id, bool isSent, DateTime sentTime, string errorMessage = "")
        {
            var result = await _dbContext.MsgMails.Include(p => p.MailAttachments).FirstOrDefaultAsync(x => x.Id == id);
            result.IsSent = isSent;
            result.SentTime = sentTime;
            result.ErrorMessage = errorMessage;
            _dbContext.Update(result);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<MailSentInfoResposeDto> GetMailSentInformationAsync(int id)
        {
            return _mapper.Map<MailSentInfoResposeDto>(await _dbContext.MsgMails.FirstOrDefaultAsync(x => x.Id == id));
        }

        public async Task SendSmtpMailAsync(CreateMailCommand mailCommand)
        {
            var mimeMessage = GetMimeMessage(mailCommand);
            using var smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync(_mailSettings.Host, _mailSettings.Port);
            await smtpClient.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
            await smtpClient.SendAsync(mimeMessage);
            await smtpClient.DisconnectAsync(true);
        }

        private MimeMessage GetMimeMessage(CreateMailCommand mailCommand)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.Sender = MailboxAddress.Parse(_mailSettings.UserName);
            mimeMessage.From.Add(new MailboxAddress(mailCommand.From.DisplayName, mailCommand.From.MailAddress));
            mailCommand.ToRecipients?.Select(x => x.MailAddress)?.ToList().ForEach(item => mimeMessage.To.Add(MailboxAddress.Parse(item)));
            mimeMessage.Subject = mailCommand.Subject;

            var builder = new BodyBuilder
            {
                HtmlBody = mailCommand.Body
            };

            mailCommand.MailAttachments?.ToList()?.ForEach(item => builder.Attachments.Add(item.Name, item.Content));
            mimeMessage.Body = builder.ToMessageBody();

            return mimeMessage;
        }

    }
}
