using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Rocky.Application.Utilities;
using Rocky.Application.Utilities.Enums;
using Rocky.Application.ViewModels;
using Rocky.Application.ViewModels.Dtos.OrderHeader;
using Rocky.Application.ViewModels.Dtos.Product;
using Rocky.Domain.Entities;
using Rocky.Domain.Interfaces.ApplicationUser;
using Rocky.Domain.Interfaces.InquiryDetail;
using Rocky.Domain.Interfaces.InquiryHeader;
using Rocky.Domain.Interfaces.OrderDetail;
using Rocky.Domain.Interfaces.OrderHeader;
using Rocky.Domain.Interfaces.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rocky.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IValidator<ApplicationUser> _applicationUserValidator;
        private readonly IOrderHeaderRepository _orderHeaderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly IProductRepository _productRepository;
        private readonly IInquiryHeaderRepository _inquiryHeaderRepository;
        private readonly IInquiryDetailRepository _inquiryDetailRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        [BindProperty]
        public ProductUserVm ProductUserVm { get; set; }

        public CartController(IWebHostEnvironment webHostEnvironment, IEmailSender emailSender, IMapper mapper,
            IProductRepository productRepository, IApplicationUserRepository applicationUserRepository,
            IInquiryHeaderRepository inquiryHeaderRepository, IInquiryDetailRepository inquiryDetailRepository,
            IValidator<ApplicationUser> applicationUserValidator, IOrderHeaderRepository orderHeaderRepository,
            IOrderDetailRepository orderDetailRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
            _mapper = mapper;
            _productRepository = productRepository;
            _applicationUserRepository = applicationUserRepository;
            _inquiryHeaderRepository = inquiryHeaderRepository;
            _inquiryDetailRepository = inquiryDetailRepository;
            _applicationUserValidator = applicationUserValidator;
            _orderHeaderRepository = orderHeaderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public IActionResult Index()
        {
            var shoppingCarts = HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstant.SessionCart) ?? new List<ShoppingCart>();

            var productsInCart = shoppingCarts.Select(i => i.ProductId).ToList();

            var products = _productRepository.Select(u => productsInCart.Contains(u.Id), p => p.Category,
                p => p.ApplicationType);

            var productDtos = _mapper.Map<List<ProductGetDto>>(products);

            foreach (var productGetDto in productDtos)
            {
                productGetDto.Sqft = shoppingCarts.FirstOrDefault(p => p.ProductId == productGetDto.Id).Sqft;
            }

            return View(productDtos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost(IEnumerable<ProductGetDto> productGetDtos)
        {
            var shoppingCarts = HttpContext.Session.Get<List<ShoppingCart>>(WebConstant.SessionCart);

            foreach (var shoppingCart in shoppingCarts)
            {
                shoppingCart.Sqft = productGetDtos.FirstOrDefault(f => f.Id == shoppingCart.ProductId).Sqft;
            }

            HttpContext.Session.Set(WebConstant.SessionCart, shoppingCarts);

            return RedirectToAction(nameof(Summary));
        }

        public IActionResult Summary()
        {
            ApplicationUser applicationUser;
            if (User.IsInRole(WebConstant.AdminRole))
            {
                var sessionInquiryId = HttpContext.Session.Get<int>(WebConstant.SessionInquiryId);
                if (!sessionInquiryId.IsDefault())
                {
                    var inquiryHeader = _inquiryHeaderRepository.FirstOrDefault(f => f.Id == sessionInquiryId);

                    applicationUser = new ApplicationUser
                    {
                        FullName = inquiryHeader.Fullname,
                        Email = inquiryHeader.Email,
                        PhoneNumber = inquiryHeader.PhoneNumber
                    };
                }
                else
                {
                    applicationUser = new ApplicationUser();
                }
            }
            else
            {

                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
                applicationUser = _applicationUserRepository.FirstOrDefault(f => f.Id == claim.Value);
            }

            var shoppingCarts = HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstant.SessionCart) ?? new List<ShoppingCart>();
            var productsInCart = shoppingCarts.Select(i => i.ProductId).ToList();

            var products = _productRepository.Select(u => productsInCart.Contains(u.Id), p => p.Category, p => p.ApplicationType);

            var productDtos = _mapper.Map<List<ProductGetDto>>(products);

            ProductUserVm = new ProductUserVm
            {
                ApplicationUser = applicationUser,
                Products = productDtos
            };

            foreach (var product in ProductUserVm.Products)
            {
                product.Sqft = shoppingCarts.FirstOrDefault(p => p.ProductId == product.Id).Sqft;
            }

            return View(ProductUserVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost(ProductUserVm productUserVm)
        {
            var validationUser = await _applicationUserValidator.ValidateAsync(productUserVm.ApplicationUser);

            if (!validationUser.IsValid)
                return View(productUserVm);

            var claimIdentity = User.Identity.AsOrDefault<ClaimsIdentity>();
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (User.IsInRole(WebConstant.AdminRole))
            {
                var totalOrderPrice = ProductUserVm.Products.Sum(p => p.Sqft * p.Price);

                var orderHeader = new OrderHeader
                {
                    CreatedBy = claim.Value,
                    TotalOrderPrice = totalOrderPrice,
                    City = ProductUserVm.ApplicationUser.City,
                    State = ProductUserVm.ApplicationUser.State,
                    StreetAddress = ProductUserVm.ApplicationUser.StreetAddress,
                    PostalCode = ProductUserVm.ApplicationUser.PostalCode,
                    Fullname = ProductUserVm.ApplicationUser.FullName,
                    PhoneNumber = ProductUserVm.ApplicationUser.PhoneNumber,
                    Email = ProductUserVm.ApplicationUser.Email,
                    OrderDate = DateTime.Now,
                    OrderStatus = Enum.GetName(OrderStatus.Pending)
                };

                _orderHeaderRepository.Add(orderHeader);

                var orderDetails = ProductUserVm.Products
                    .Select(p => new OrderDetail
                    {
                        OrderHeaderId = orderHeader.Id,
                        ProductId = p.Id,
                        Sqft = p.Sqft
                    }).ToList();

                foreach (var orderDetail in orderDetails)
                    _orderDetailRepository.Add(orderDetail, false);

                _orderDetailRepository.SaveChanges();

                TempData[WebConstant.Succeed] = WebConstant.MissionComplete;

                return RedirectToAction(nameof(InquiryConfirmation), new { orderHeader.Id });
            }
            else
            {
                const string subject = "New Inquiry";
                var pathToTemplate = $"{_webHostEnvironment.WebRootPath}{Path.DirectorySeparatorChar}templates{Path.DirectorySeparatorChar}Inquiry.html";
                string htmlBody;

                using (var streamReader = System.IO.File.OpenText(pathToTemplate)) htmlBody = await streamReader.ReadToEndAsync();

                var builder = new StringBuilder();
                foreach (var prod in productUserVm.Products)
                    builder.Append(
                        $" - Name: {prod.Name} <span style='font-size:14px;'> (ID: {prod.Id})</span><br />");

                var messageBody = htmlBody.FormatWith(productUserVm.ApplicationUser.FullName,
                    productUserVm.ApplicationUser.Email, productUserVm.ApplicationUser.PhoneNumber, builder);

                await _emailSender.SendEmailAsync(WebConstant.EmailAdmin, subject, messageBody);

                var inquiryHeader = new InquiryHeader
                {
                    ApplicationUserId = claim?.Value,
                    Fullname = productUserVm.ApplicationUser.FullName,
                    PhoneNumber = productUserVm.ApplicationUser.PhoneNumber,
                    Email = productUserVm.ApplicationUser.Email
                };

                _inquiryHeaderRepository.Add(inquiryHeader);

                foreach (var productGetDto in productUserVm.Products)
                {
                    _inquiryDetailRepository.Add(new InquiryDetail
                    {
                        InquiryHeaderId = inquiryHeader.Id,
                        ProductId = productGetDto.Id
                    }, false);
                }

                _inquiryDetailRepository.SaveChanges();

                TempData[WebConstant.Succeed] = WebConstant.MissionComplete;

                return RedirectToAction(nameof(InquiryConfirmation));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCart(IEnumerable<ProductGetDto> productGetDtos)
        {
            var shoppingCarts = HttpContext.Session.Get<List<ShoppingCart>>(WebConstant.SessionCart);

            foreach (var shoppingCart in shoppingCarts)
            {
                shoppingCart.Sqft = productGetDtos.FirstOrDefault(f => f.Id == shoppingCart.ProductId).Sqft;
            }

            HttpContext.Session.Set(WebConstant.SessionCart, shoppingCarts);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult InquiryConfirmation(int id = 0)
        {
            var orderHeader = _orderHeaderRepository.FirstOrDefault(o => o.Id == id);

            var orderHeaderDto = _mapper.Map<OrderHeaderGetDto>(orderHeader);

            HttpContext.Session.Clear();
            TempData[WebConstant.Succeed] = WebConstant.MissionComplete;
            return View(orderHeaderDto);
        }

        public IActionResult Remove(int id)
        {
            var shoppingCarts = HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstant.SessionCart).ToList() ?? new List<ShoppingCart>();

            shoppingCarts.Remove(shoppingCarts.FirstOrDefault(u => u.ProductId == id));

            HttpContext.Session.Set(WebConstant.SessionCart, shoppingCarts);
            TempData[WebConstant.Succeed] = WebConstant.MissionComplete;

            return RedirectToAction(nameof(Index));
        }
    }
}
