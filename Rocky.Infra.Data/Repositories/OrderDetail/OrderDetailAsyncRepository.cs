using Rocky.Domain.Interfaces.OrderDetail;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Repositories.Common;
using Rocky.Infra.Data.Scrutor;

namespace Rocky.Infra.Data.Repositories.OrderDetail
{
    public class OrderDetailAsyncRepository : AsyncRepository<Domain.Entities.OrderDetail, int>, IScoped, IOrderDetailAsyncRepository
    {
        public OrderDetailAsyncRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}