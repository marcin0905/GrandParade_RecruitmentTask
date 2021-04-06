using System;

namespace Common.Domain
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        protected bool Equals(BaseEntity other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BaseEntity) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}