using System;
using System.Collections.Generic;

namespace PS.Notification.Domain.Entities
{
    public class MsgMail
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string ApplicationName { get; set; }
        public string FromDisplayName { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public virtual IEnumerable<MsgMailAttachment> MailAttachments { get; set; } = new List<MsgMailAttachment>();
        public DateTime Created { get; set; }
        public bool IsSent { get; set; }
        public DateTime? SendTime { get; set; }
        public string ErrorMessage { get; set; }
    }
}
