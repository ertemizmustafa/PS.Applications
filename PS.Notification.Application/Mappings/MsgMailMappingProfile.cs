using AutoMapper;
using PS.Notification.Abstractions;
using PS.Notification.Application.Dtos;
using PS.Notification.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PS.Notification.Application.Mappings
{
    public class MsgMailMappingProfile : Profile
    {
        public MsgMailMappingProfile()
        {
            CreateMap<CreateMailCommand, MsgMail>()
                    .ForMember(d => d.ApplicationName, o => o.MapFrom(x => x.ApplicationName))
                    .ForMember(d => d.FromMailAddress, o => o.MapFrom(x => x.From.MailAddress))
                    .ForMember(d => d.FromDisplayName, o => o.MapFrom(x => x.From.DisplayName))
                    .ForMember(d => d.Subject, o => o.MapFrom(x => x.Subject))
                    .ForMember(d => d.Body, o => o.MapFrom(x => x.Body))
                    .ForMember(d => d.ToRecipients, o => o.MapFrom(x => string.Join(",", x.ToRecipients.Select(x => x.MailAddress))))
                    .ForMember(d => d.CcRecipients, o => o.MapFrom(x => string.Join(",", x.CcRecipients.Select(x => x.MailAddress))));
            CreateMap<CreateMailCommand, IEnumerable<MsgMailAttachment>>()
                     .ConstructUsing(s => s.MailAttachments.Select(x => new MsgMailAttachment { Name = x.Name, Content = x.Content }));

            CreateMap<MsgMail, MailSentInfoResposeDto>()
                .ForMember(d => d.MailId, o => o.MapFrom(x => x.Id))
                .ForMember(d => d.IsSent, o => o.MapFrom(x => x.IsSent))
                .ForMember(d => d.HasSentError, o => o.MapFrom(x => !string.IsNullOrEmpty(x.ErrorMessage)))
                .ForMember(d => d.SentTime, o => o.MapFrom(x => x.SentTime));
        }
    }
}
