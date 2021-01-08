using Rocky.Domain.Common;
using System.Collections.Generic;

namespace Rocky.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Picture { get; set; }
        public int CategoryId { get; set; }
        public int ApplicationTypeId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ApplicationType ApplicationType { get; set; }
        public List<InquiryDetail> InquiryDetails { get; set; }
    }
}
