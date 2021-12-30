using System;
using System.Collections.Generic;

namespace PS.Notification.Domain.Entities
{
    public class MsgMail
    {
        public int Id { get; set; }
        public string ApplicationName { get; set; }
        public string FromMailAddress { get; set; }
        public string FromDisplayName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ToRecipients { get; set; }
        public string CcRecipients { get; set; }
        public virtual IEnumerable<MsgMailAttachment> MailAttachments { get; set; } = new List<MsgMailAttachment>();
        public DateTime Created { get; set; }
        public bool IsSent { get; set; }
        public DateTime? SentTime { get; set; }
        public string ErrorMessage { get; set; }
    }
}
