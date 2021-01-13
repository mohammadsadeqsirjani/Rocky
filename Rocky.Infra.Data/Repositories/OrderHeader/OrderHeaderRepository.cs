using Rocky.Domain.Interfaces.OrderHeader;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Repositories.Common;
using Rocky.Infra.Data.Scrutor;

namespace Rocky.Infra.Data.Repositories.OrderHeader
{
    public class OrderHeaderRepository : Repository<Domain.Entities.OrderHeader, int>, IScoped, IOrderHeaderRepository
    {
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
