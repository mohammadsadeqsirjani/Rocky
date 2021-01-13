using Rocky.Domain.Interfaces.ApplicationUser;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Repositories.Common;
using Rocky.Infra.Data.Scrutor;

namespace Rocky.Infra.Data.Repositories.ApplicationUser
{
    public class ApplicationUserRepository : Repository<Domain.Entities.ApplicationUser, string>, IScoped, IApplicationUserRepository
    {
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
