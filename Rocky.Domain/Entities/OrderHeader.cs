using Rocky.Domain.Common;
using System;
using System.Collections.Generic;

namespace Rocky.Domain.Entities
{
    public class OrderHeader : BaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public double TotalOrderPrice { get; set; }
        public string OrderStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public string TransactionId { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
