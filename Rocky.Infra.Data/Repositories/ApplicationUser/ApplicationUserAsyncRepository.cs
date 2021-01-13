using Rocky.Domain.Interfaces.ApplicationUser;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Repositories.Common;
using Rocky.Infra.Data.Scrutor;

namespace Rocky.Infra.Data.Repositories.ApplicationUser
{
    public class ApplicationUserAsyncRepository : AsyncRepository<Domain.Entities.ApplicationUser, string>, IScoped, IApplicationUserAsyncRepository
    {
        public ApplicationUserAsyncRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
