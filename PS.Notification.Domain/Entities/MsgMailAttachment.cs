namespace PS.Notification.Domain.Entities
{
    public class MsgMailAttachment
    {
        public int Id { get; set; }
        public int MailId { get; set; }
        public virtual MsgMail Mail { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}
