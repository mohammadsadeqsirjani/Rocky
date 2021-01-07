using Rocky.Domain.Interfaces.Product;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Repositories.Common;

namespace Rocky.Infra.Data.Repositories.Product
{
    public class ProductAsyncRepository : AsyncRepository<Domain.Entities.Product>, IProductAsyncRepository
    {
        public ProductAsyncRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
