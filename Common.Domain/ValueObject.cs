namespace Common.Domain
{
    public abstract class ValueObject<TValueObject> where TValueObject: ValueObject<TValueObject>
    {
        protected bool Equals(TValueObject other)
        {
            return EqualsCore(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            return obj.GetType() == this.GetType() && Equals((TValueObject) obj);
        }

        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        protected abstract bool EqualsCore(TValueObject valueObject);
        protected abstract int GetHashCodeCore();
    }
}