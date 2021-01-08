using Rocky.Domain.Interfaces.Common;

namespace Rocky.Domain.Interfaces.ApplicationUser
{
    public interface IApplicationUserRepository : IRepository<Entities.ApplicationUser, string>
    {
    }
}
