using System;

namespace PS.Notification.Application.Dtos
{
    public class MailSentInfoResposeDto
    {
        public int MailId { get; set; }
        public bool IsSent { get; set; }
        public DateTime? SentTime { get; set; }
        public bool HasSentError { get; set; }
    }
}
