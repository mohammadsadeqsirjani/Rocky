using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Rocky.Application.Utilities;
using Rocky.Application.ViewModels;
using Rocky.Application.ViewModels.Dtos.Product;
using Rocky.Domain.Entities;
using Rocky.Domain.Interfaces.Product;
using Rocky.Infra.Data.Persistence;
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
        private ApplicationDbContext _db;
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        [BindProperty]
        public ProductUserVm ProductUserVm { get; set; }
        public CartController(IWebHostEnvironment webHostEnvironment, IEmailSender emailSender, IMapper mapper, IProductRepository productRepository, ApplicationDbContext db)
        {
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
            _mapper = mapper;
            _productRepository = productRepository;
            _db = db;
        }

        public IActionResult Index()
        {
            var shoppingCarts = HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstant.SessionCart) ?? new List<ShoppingCart>();

            var productsInCart = shoppingCarts.Select(i => i.ProductId).ToList();

            var products = _productRepository.Select(u => productsInCart.Contains(u.Id));

            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            return RedirectToAction(nameof(Summary));
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            var shoppingCarts = HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstant.SessionCart) ?? new List<ShoppingCart>();
            var productsInCart = shoppingCarts.Select(i => i.ProductId).ToList();

            var products = _productRepository.Select(u => productsInCart.Contains(u.Id), p => p.Category, p => p.ApplicationType);

            var productDtos = _mapper.Map<List<ProductGetDto>>(products);

            ProductUserVm = new ProductUserVm
            {
                ApplicationUser = _db.ApplicationUser.FirstOrDefault(u => u.Id == claim.Value),
                Products = productDtos
            };

            return View(ProductUserVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost(ProductUserVm productUserVm)
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

            return RedirectToAction(nameof(InquiryConfirmation));
        }

        public IActionResult InquiryConfirmation()
        {
            HttpContext.Session.Clear();
            return View();
        }

        public IActionResult Remove(int id)
        {
            var shoppingCarts = HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstant.SessionCart).ToList() ?? new List<ShoppingCart>();

            shoppingCarts.Remove(shoppingCarts.FirstOrDefault(u => u.ProductId == id));

            HttpContext.Session.Set(WebConstant.SessionCart, shoppingCarts);

            return RedirectToAction(nameof(Index));
        }
    }
}
