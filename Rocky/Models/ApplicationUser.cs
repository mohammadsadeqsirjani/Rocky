using Microsoft.AspNetCore.Identity;

namespace Rocky.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
