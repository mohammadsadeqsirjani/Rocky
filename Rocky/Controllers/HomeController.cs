using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocky.Application.Dtos.Category;
using Rocky.Application.Dtos.Product;
using Rocky.Application.Utilities;
using Rocky.Application.ViewModels;
using Rocky.Domain.Entities;
using Rocky.Infra.Data.Persistence;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Rocky.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public HomeController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = _db.Products
                .Include(u => u.Category)
                .Include(u => u.ApplicationType);

            var productDtos = _mapper.Map<IEnumerable<ProductGetDto>>(products);

            var categories = _db.Categories;

            var categoryDtos = _mapper.Map<IEnumerable<CategoryGetDto>>(categories);

            var homeVm = new HomeVm
            {
                Products = productDtos,
                Categories = categoryDtos
            };

            return View(homeVm);
        }

        public IActionResult Details(int id)
        {
            var shoppingCarts = HttpContext.Session.Get<List<ShoppingCart>>(WebConstant.SessionCart) ?? new List<ShoppingCart>();

            var product = _db.Products
                .Include(u => u.Category)
                .Include(u => u.ApplicationType)
                .FirstOrDefault(u => u.Id == id);

            var productDto = _mapper.Map<ProductGetDto>(product);

            var detailsVm = new DetailsVm
            {
                Product = productDto,
                ExistsInCart = false
            };

            foreach (var _ in shoppingCarts.Where(item => item.ProductId == id))
                detailsVm.ExistsInCart = true;

            return View(detailsVm);
        }

        [HttpPost, ActionName("Details")]
        public IActionResult DetailsPost(int id)
        {
            var shoppingCarts = HttpContext.Session.Get<List<ShoppingCart>>(WebConstant.SessionCart) ?? new List<ShoppingCart>();

            shoppingCarts.Add(new ShoppingCart { ProductId = id });

            HttpContext.Session.Set(WebConstant.SessionCart, shoppingCarts);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int id)
        {
            var shoppingCarts = HttpContext.Session.Get<List<ShoppingCart>>(WebConstant.SessionCart) ?? new List<ShoppingCart>();

            var itemToRemove = shoppingCarts.FirstOrDefault(r => r.ProductId == id);

            if (itemToRemove != null)
                shoppingCarts.Remove(itemToRemove);

            HttpContext.Session.Set(WebConstant.SessionCart, shoppingCarts);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
