using Rocky.Application.ViewModels.Dtos.OrderDetail;
using Rocky.Application.ViewModels.Dtos.OrderHeader;
using System.Collections.Generic;

namespace Rocky.Application.ViewModels
{
    public class OrderVm
    {
        public OrderHeaderGetDto OrderHeader { get; set; }
        public IEnumerable<OrderDetailGetDto> OrderDetails { get; set; }
    }
}
