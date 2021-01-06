using Microsoft.AspNetCore.Identity;

namespace Rocky.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
