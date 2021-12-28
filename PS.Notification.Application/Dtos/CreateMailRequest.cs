using System.Collections.Generic;

namespace PS.Notification.Application.Dtos
{
    public class CreateMailRequest
    {
        public string ExternalId { get; set; }
        public string ApplicationName { get; set; }
        public string FromDisplayName { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> To { get; set; }
        public IEnumerable<string> Cc { get; set; }
        public IEnumerable<MailAttachment> MailAttachments { get; set; }
    }

    public class MailAttachment
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}
