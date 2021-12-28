using AutoMapper;
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
            CreateMap<CreateMailRequest, MsgMail>()
                .ForMember(d => d.ExternalId, o => o.MapFrom(x => x.ExternalId))
                .ForMember(d => d.ApplicationName, o => o.MapFrom(x => x.ApplicationName))
                .ForMember(d => d.FromDisplayName, o => o.MapFrom(x => x.FromDisplayName))
                .ForMember(d => d.From, o => o.MapFrom(x => x.From))
                .ForMember(d => d.Subject, o => o.MapFrom(x => x.Subject))
                .ForMember(d => d.Body, o => o.MapFrom(x => x.Body))
                .ForMember(d => d.To, o => o.MapFrom(x => string.Join(';', x.To)))
                .ForMember(d => d.Cc, o => o.MapFrom(x => string.Join(';', x.Cc)));
            CreateMap<CreateMailRequest, IEnumerable<MsgMailAttachment>>()
                   .ConstructUsing(s => s.MailAttachments.Select(x => new MsgMailAttachment { Name = x.Name, Content = x.Content }));
        }
    }
}
