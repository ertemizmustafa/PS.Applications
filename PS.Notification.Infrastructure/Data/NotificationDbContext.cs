using Microsoft.EntityFrameworkCore;
using PS.Notification.Domain.Entities;
using System.Reflection;

namespace PS.Notification.Infrastructure.Data
{
    public class NotificationDbContext : DbContext
    {
        public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
        {

        }

        public DbSet<MsgMail> MsgMails { get; set; }
        public DbSet<MsgMailAttachment> MsgMailAttachments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("PSN");
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
