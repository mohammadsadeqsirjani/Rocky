using Microsoft.AspNetCore.Identity;
using Rocky.Domain.Common;
using System.Collections.Generic;

namespace Rocky.Domain.Entities
{
    public class ApplicationUser : IdentityUser, IBaseEntity<string>
    {
        public string FullName { get; set; }

        public List<InquiryHeader> InquiryHeaders { get; set; }
    }
}
