using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PS.Notification.Domain.Entities;

namespace PS.Notification.Infrastructure.Data.Configurations
{
    public class MsgMailAttachmentConfiguration : IEntityTypeConfiguration<MsgMailAttachment>
    {
        public void Configure(EntityTypeBuilder<MsgMailAttachment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Mail)
                .WithMany(x => x.MailAttachments)
                .HasForeignKey(x => x.MailId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
