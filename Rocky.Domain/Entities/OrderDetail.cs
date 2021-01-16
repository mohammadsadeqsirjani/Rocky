using Rocky.Domain.Common;

namespace Rocky.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int OrderHeaderId { get; set; }
        public int ProductId { get; set; }
        public int Sqft { get; set; }

        public OrderHeader OrderHeader { get; set; }
        public Product Product { get; set; }
    }
}
