using Microsoft.AspNetCore.Mvc.Rendering;
using Rocky.Application.ViewModels.Dtos.OrderHeader;
using System;
using System.Collections.Generic;

namespace Rocky.Application.ViewModels
{
    public class OrderInputVm
    {
        public IEnumerable<OrderHeaderGetDto> OrderHeaders { get; set; }

        public IEnumerable<SelectListItem> OrderStatus
        {
            get
            {
                var approved = Enum.GetName(Utilities.Enums.OrderStatus.Approved);
                var cancelled = Enum.GetName(Utilities.Enums.OrderStatus.Cancelled);
                var inProcess = Enum.GetName(Utilities.Enums.OrderStatus.InProcess);
                var pending = Enum.GetName(Utilities.Enums.OrderStatus.Pending);
                var refunded = Enum.GetName(Utilities.Enums.OrderStatus.Refunded);
                var shipped = Enum.GetName(Utilities.Enums.OrderStatus.Shipped);

                yield return new SelectListItem(approved, approved);
                yield return new SelectListItem(cancelled, cancelled);
                yield return new SelectListItem(inProcess, inProcess);
                yield return new SelectListItem(pending, pending);
                yield return new SelectListItem(refunded, refunded);
                yield return new SelectListItem(shipped, shipped);

            }
        }

        public string Status { get; set; }
    }
}
