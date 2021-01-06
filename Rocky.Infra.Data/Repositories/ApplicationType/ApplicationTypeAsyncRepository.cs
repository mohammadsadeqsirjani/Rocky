using Rocky.Domain.Interfaces.ApplicationType;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Repositories.Common;

namespace Rocky.Infra.Data.Repositories.ApplicationType
{
    public class ApplicationTypeRepository : Repository<Domain.Entities.ApplicationType>, IApplicationTypeRepository
    {
        public ApplicationTypeRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
