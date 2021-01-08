using Rocky.Domain.Interfaces.Category;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Repositories.Common;

namespace Rocky.Infra.Data.Repositories.Category
{
    public class CategoryRepository : Repository<Domain.Entities.Category, int>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
