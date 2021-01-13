using Rocky.Domain.Interfaces.Product;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Repositories.Common;
using Rocky.Infra.Data.Scrutor;

namespace Rocky.Infra.Data.Repositories.Product
{
    public class ProductAsyncRepository : AsyncRepository<Domain.Entities.Product, int>, IScoped, IProductAsyncRepository
    {
        public ProductAsyncRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
