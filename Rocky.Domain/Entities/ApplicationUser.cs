using Microsoft.AspNetCore.Identity;
using Rocky.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rocky.Domain.Entities
{
    public class ApplicationUser : IdentityUser, IBaseEntity<string>
    {
        public string FullName { get; set; }

        [NotMapped] public string StreetAddress { get; set; }
        [NotMapped] public string City { get; set; }
        [NotMapped] public string State { get; set; }
        [NotMapped] public string PostalCode { get; set; }

        public List<InquiryHeader> InquiryHeaders { get; set; }
        public List<OrderHeader> OrderHeaders { get; set; }
    }
}
