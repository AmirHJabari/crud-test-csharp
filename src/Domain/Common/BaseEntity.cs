namespace Domain.Common;

public abstract class BaseEntity<TKey>
    where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
{
    public virtual TKey Id { get; set; }
}

public abstract class BaseEntity : BaseEntity<int>
{

}