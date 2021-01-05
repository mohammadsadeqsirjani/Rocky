using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rocky.Data;
using Rocky.Models;
using Rocky.Models.ViewModels;
using Rocky.Utility;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Rocky.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            var homeVm = new HomeVm
            {
                Products = _db.Products
                    .Include(u => u.Category)
                    .Include(u => u.ApplicationType),
                Categories = _db.Categories
            };
            return View(homeVm);
        }

        public IActionResult Details(int id)
        {
            var shoppingCarts = HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstant.SessionCart) ?? new List<ShoppingCart>();

            var detailsVm = new DetailsVm
            {
                Product = _db.Products
                    .Include(u => u.Category)
                    .Include(u => u.ApplicationType)
                    .FirstOrDefault(u => u.Id == id),
                ExistsInCart = false
            };

            foreach (var _ in shoppingCarts.Where(item => item.ProductId == id))
                detailsVm.ExistsInCart = true;

            return View(detailsVm);
        }

        [HttpPost, ActionName("Details")]
        public IActionResult DetailsPost(int id)
        {
            var shoppingCarts = HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstant.SessionCart).ToList() ?? new List<ShoppingCart>();

            shoppingCarts.Add(new ShoppingCart { ProductId = id });

            HttpContext.Session.Set(WebConstant.SessionCart, shoppingCarts);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int id)
        {
            var shoppingCarts = HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WebConstant.SessionCart).ToList() ?? new List<ShoppingCart>();

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
