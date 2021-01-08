using Rocky.Domain.Common;
using System.Collections.Generic;

namespace Rocky.Domain.Entities
{
    public class InquiryHeader : BaseEntity
    {
        public string ApplicationUserId { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public List<InquiryDetail> InquiryDetails { get; set; }
    }
}
