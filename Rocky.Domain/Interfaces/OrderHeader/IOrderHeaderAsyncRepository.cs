using Rocky.Domain.Interfaces.Common;

namespace Rocky.Domain.Interfaces.OrderHeader
{
    public interface IOrderHeaderAsyncRepository : IAsyncRepository<Entities.OrderHeader, int>
    {
    }
}