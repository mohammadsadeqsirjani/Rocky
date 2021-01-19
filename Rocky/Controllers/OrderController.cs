using AutoMapper;
using Braintree;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rocky.Application.Utilities;
using Rocky.Application.Utilities.BrainTree;
using Rocky.Application.Utilities.Enums;
using Rocky.Application.ViewModels;
using Rocky.Application.ViewModels.Dtos.OrderDetail;
using Rocky.Application.ViewModels.Dtos.OrderHeader;
using Rocky.Domain.Interfaces.OrderDetail;
using Rocky.Domain.Interfaces.OrderHeader;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rocky.Controllers
{
    [Authorize(Roles = WebConstant.AdminRole)]
    public class OrderController : Controller
    {
        private readonly IOrderHeaderRepository _orderHeaderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IBrainTreeGateway _brainTreeGateway;
        private readonly IMapper _mapper;

        [BindProperty] public OrderVm OrderVm { get; set; }

        public OrderController(IOrderHeaderRepository orderHeaderRepository,
            IOrderDetailRepository orderDetailRepository, IBrainTreeGateway brainTreeGateway, IMapper mapper)
        {
            _orderHeaderRepository = orderHeaderRepository;
            _orderDetailRepository = orderDetailRepository;
            _brainTreeGateway = brainTreeGateway;
            _mapper = mapper;
        }

        public IActionResult Index(string searchName = null, string searchEmail = null, string searchPhone = null, [JsonProperty("Status")] string status = null)
        {
            var orderHeaders = _orderHeaderRepository.Select();

            var orderHeaderDto = _mapper.Map<IEnumerable<OrderHeaderGetDto>>(orderHeaders);

            if (searchName.IsNotNullOrEmpty())
                orderHeaderDto = orderHeaderDto.Where(x => x.Fullname.Contains(searchName!, StringComparison.OrdinalIgnoreCase));
            if (searchEmail.IsNotNullOrEmpty())
                orderHeaderDto = orderHeaderDto.Where(x => x.Email.Contains(searchEmail!, StringComparison.OrdinalIgnoreCase));
            if (searchPhone.IsNotNullOrEmpty())
                orderHeaderDto = orderHeaderDto.Where(x => x.PhoneNumber.Contains(searchPhone!, StringComparison.OrdinalIgnoreCase));
            if (status.IsNotNullOrEmpty() && !status.Equals("--Order Status--"))
                orderHeaderDto = orderHeaderDto.Where(x => x.OrderStatus.Contains(status!, StringComparison.OrdinalIgnoreCase));

            var orderVm = new OrderInputVm
            {
                OrderHeaders = orderHeaderDto
            };

            return View(orderVm);
        }

        public IActionResult Details(int id)
        {
            var orderHeader = _orderHeaderRepository.FirstOrDefault(x => x.Id == id);
            var orderDetails = _orderDetailRepository.Select(x => x.OrderHeaderId == id, x => x.Product);

            var orderHeaderDto = _mapper.Map<OrderHeaderGetDto>(orderHeader);
            var orderDetailDto = _mapper.Map<IEnumerable<OrderDetailGetDto>>(orderDetails);

            OrderVm = new OrderVm
            {
                OrderHeader = orderHeaderDto,
                OrderDetails = orderDetailDto
            };

            return View(OrderVm);
        }

        [HttpPost]
        public IActionResult UpdateOrderDetails()
        {
            var orderHeader = _orderHeaderRepository.FirstOrDefault(x => x.Id == OrderVm.OrderHeader.Id);

            orderHeader.Fullname = OrderVm.OrderHeader.Fullname;
            orderHeader.PhoneNumber = OrderVm.OrderHeader.PhoneNumber;
            orderHeader.Email = OrderVm.OrderHeader.Email;
            orderHeader.StreetAddress = OrderVm.OrderHeader.StreetAddress;
            orderHeader.City = OrderVm.OrderHeader.City;
            orderHeader.State = OrderVm.OrderHeader.State;
            orderHeader.PostalCode = OrderVm.OrderHeader.PostalCode;

            _orderHeaderRepository.Update(orderHeader);

            TempData[WebConstant.Succeed] = "Order details updated successfully";

            return RedirectToAction("Details", "Order", new { orderHeader.Id });
        }

        [HttpPost]
        public IActionResult StartProcessing()
        {
            var orderHeader = _orderHeaderRepository.FirstOrDefault(x => x.Id == OrderVm.OrderHeader.Id);

            orderHeader.OrderStatus = Enum.GetName(OrderStatus.InProcess);

            _orderHeaderRepository.Update(orderHeader);

            TempData[WebConstant.Succeed] = "Order is in process";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ShipOrder()
        {
            var orderHeader = _orderHeaderRepository.FirstOrDefault(x => x.Id == OrderVm.OrderHeader.Id);

            orderHeader.OrderStatus = Enum.GetName(OrderStatus.Shipped);
            orderHeader.ShippingDate = DateTime.Now;

            _orderHeaderRepository.Update(orderHeader);

            TempData[WebConstant.Succeed] = "Order shipped successfully";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult CancelOrder()
        {
            var orderHeader = _orderHeaderRepository.FirstOrDefault(x => x.Id == OrderVm.OrderHeader.Id);

            var gateway = _brainTreeGateway.GetGateway();
            if (orderHeader.TransactionId.IsNullOrEmpty())
            {
                orderHeader.OrderStatus = Enum.GetName(OrderStatus.Cancelled);
            }
            else
            {
                var transaction = gateway.Transaction.Find(orderHeader.TransactionId);

                if (transaction.Status == TransactionStatus.AUTHORIZED ||
                    transaction.Status == TransactionStatus.SUBMITTED_FOR_SETTLEMENT)
                {
                    var resultVoid = gateway.Transaction.Void(orderHeader.TransactionId);
                }
                else
                {
                    var resultRefund = gateway.Transaction.Refund(orderHeader.TransactionId);
                }

                orderHeader.OrderStatus = Enum.GetName(OrderStatus.Refunded);
            }

            _orderHeaderRepository.Update(orderHeader);

            TempData[WebConstant.Succeed] = "Order cancelled successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
