using Rocky.Domain.Interfaces.Product;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Repositories.Common;

namespace Rocky.Infra.Data.Repositories.Product
{
    public class ProductRepository : Repository<Domain.Entities.Product, int>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
