using System;

namespace Rocky.Application.Utilities.Enums
{
    [Flags]
    public enum OrderStatus
    {
        Pending,
        Approved,
        InProcess,
        Shipped,
        Cancelled,
        Refunded
    }
}
