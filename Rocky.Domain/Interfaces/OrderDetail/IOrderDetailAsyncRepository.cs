using Rocky.Domain.Interfaces.Common;

namespace Rocky.Domain.Interfaces.OrderDetail
{
    public interface IOrderDetailAsyncRepository : IAsyncRepository<Entities.OrderDetail, int>
    {
    }
}