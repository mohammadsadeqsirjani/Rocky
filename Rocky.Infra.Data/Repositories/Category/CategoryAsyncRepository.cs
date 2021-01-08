using Rocky.Domain.Interfaces.Category;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Repositories.Common;

namespace Rocky.Infra.Data.Repositories.Category
{
    public class CategoryAsyncRepository : AsyncRepository<Domain.Entities.Category, int>, ICategoryAsyncRepository
    {
        public CategoryAsyncRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
