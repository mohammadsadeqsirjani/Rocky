using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Rocky.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public List<InquiryHeader> InquiryHeaders { get; set; }
    }
}
