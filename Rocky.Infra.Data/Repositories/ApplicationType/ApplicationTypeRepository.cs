using Rocky.Domain.Interfaces.ApplicationType;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Repositories.Common;

namespace Rocky.Infra.Data.Repositories.ApplicationType
{
    public class ApplicationTypeAsyncRepository : AsyncRepository<Domain.Entities.ApplicationType>, IApplicationTypeAsyncRepository
    {
        public ApplicationTypeAsyncRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
