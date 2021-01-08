using Rocky.Domain.Interfaces.Common;

namespace Rocky.Domain.Interfaces.ApplicationUser
{
    public interface IApplicationUserAsyncRepository : IAsyncRepository<Entities.ApplicationUser, string>
    {
    }
}
