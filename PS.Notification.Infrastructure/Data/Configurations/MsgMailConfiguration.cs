using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PS.Notification.Domain.Entities;

namespace PS.Notification.Infrastructure.Data.Configurations
{
    public class MsgMailConfiguration : IEntityTypeConfiguration<MsgMail>
    {
        public void Configure(EntityTypeBuilder<MsgMail> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Created).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(x => x.IsSent).HasDefaultValue(false);
        }
    }
}
