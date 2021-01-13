using Rocky.Domain.Interfaces.OrderHeader;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Repositories.Common;
using Rocky.Infra.Data.Scrutor;

namespace Rocky.Infra.Data.Repositories.OrderHeader
{
    public class OrderHeaderAsyncRepository : AsyncRepository<Domain.Entities.OrderHeader, int>, IScoped, IOrderHeaderAsyncRepository
    {
        public OrderHeaderAsyncRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}