using Rocky.Domain.Interfaces.ApplicationType;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Repositories.Common;
using Rocky.Infra.Data.Scrutor;

namespace Rocky.Infra.Data.Repositories.ApplicationType
{
    public class ApplicationTypeRepository : Repository<Domain.Entities.ApplicationType, int>, IScoped, IApplicationTypeRepository
    {
        public ApplicationTypeRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
