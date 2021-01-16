using Rocky.Domain.Interfaces.OrderDetail;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Repositories.Common;
using Rocky.Infra.Data.Scrutor;

namespace Rocky.Infra.Data.Repositories.OrderDetail
{
    public class OrderDetailRepository : Repository<Domain.Entities.OrderDetail, int>, IScoped, IOrderDetailRepository
    {
        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
