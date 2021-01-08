namespace Rocky.Domain.Common
{
    public interface IBaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }

    public interface IBaseEntity : IBaseEntity<int>
    {

    }
}
