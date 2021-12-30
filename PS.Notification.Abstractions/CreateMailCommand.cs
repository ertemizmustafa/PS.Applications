using System.Collections.Generic;

namespace PS.Notification.Abstractions
{
    /// <summary>
    /// Contains mail information
    /// </summary>
    public class CreateMailCommand
    {
        public MailTemplate MailTemplate { get; set; }
        public string ApplicationName { get; set; }
        public MailContact From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public IEnumerable<MailContact> ToRecipients { get; set; }
        public IEnumerable<MailContact> CcRecipients { get; set; }
        public IEnumerable<MailAttachment> MailAttachments { get; set; }
    }

    public class MailTemplate
    {
        public string Domain { get; set; }
        public string Name { get; set; }
        public object Data { get; set; }
    }

    public class MailAttachment
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }

    public class MailContact
    {
        public string MailAddress { get; set; }
        public string DisplayName { get; set; }
    }
}
