namespace Rocky.Domain.Common
{
    public class BaseEntity<TKey> : IBaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }

    public class BaseEntity : BaseEntity<int>, IBaseEntity
    {

    }
}
