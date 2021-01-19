using Rocky.Application.ViewModels.Dtos.Common;

namespace Rocky.Application.ViewModels.Dtos.OrderDetail
{
    public class OrderDetailGetDto : EntityGetDto
    {
        public int OrderHeaderId { get; set; }
        public int ProductId { get; set; }
        public int Sqft { get; set; }
        public string ProductName { get; set; }
        public double PricePerSqft { get; set; }
    }
}
