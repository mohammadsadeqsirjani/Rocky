using Rocky.Domain.Common;

namespace Rocky.Domain.Entities
{
    public class InquiryDetail : BaseEntity
    {
        public int InquiryHeaderId { get; set; }
        public int ProductId { get; set; }

        public InquiryHeader InquiryHeader { get; set; }
        public Product Product { get; set; }
    }
}
