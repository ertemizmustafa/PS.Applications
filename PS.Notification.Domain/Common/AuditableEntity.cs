using System;

namespace PS.Notification.Domain.Common
{
    public interface IEntity
    {

    }
    public interface IEntityUpdate
    {
        DateTime? LastModified { get; set; }

        string LastModifiedBy { get; set; }
    }

    public interface ISoftDelete
    {
        DateTime? Deleted { get; set; }
        string DeletedBy { get; set; }
    }

    public abstract class AuditableEntity : IEntity
    {
        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }
    }
    public abstract class AuditableUpdateDeleteEntity : AuditableEntity, IEntity, IEntityUpdate
    {
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }
    }

    public abstract class AuditableSoftDeleteEntity : AuditableEntity, ISoftDelete
    {
        public DateTime? Deleted { get; set; }
        public string DeletedBy { get; set; }

    }

}
