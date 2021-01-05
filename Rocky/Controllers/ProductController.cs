using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rocky.Data;
using Rocky.Models;
using Rocky.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rocky.Controllers
{
    [Authorize(Roles = WebConstant.AdminRole)]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            IEnumerable<Product> products = _db.Products
                .Include(u => u.Category)
                .Include(u => u.ApplicationType);

            return View(products);
        }

        public IActionResult Upsert(int? id)
        {
            var productVm = new ProductVm
            {
                Product = new Product(),
                Categories = _db.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                ApplicationTypes = _db.ApplicationTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null)
                return View(productVm);

            productVm.Product = _db.Products.Find(id);

            if (productVm.Product == null)
                return NotFound();

            return View(productVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVm productVm)
        {
            if (!ModelState.IsValid)
            {
                productVm.Categories = _db.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

                productVm.ApplicationTypes = _db.ApplicationTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });

                return View(productVm);
            }

            var files = HttpContext.Request.Form.Files;
            var webRootPath = _webHostEnvironment.WebRootPath;

            if (productVm.Product.Id == 0)
            {
                var upload = webRootPath + WebConstant.ImagePath;
                var fileName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    files[0].CopyTo(fileStream);

                productVm.Product.Picture = fileName + extension;

                _db.Products.Add(productVm.Product);
            }
            else
            {
                var product = _db.Products.AsNoTracking().FirstOrDefault(u => u.Id == productVm.Product.Id);

                if (files.Any())
                {
                    var upload = webRootPath + WebConstant.ImagePath;
                    var fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(files[0].FileName);

                    var oldFile = Path.Combine(upload, product.Picture);

                    if (System.IO.File.Exists(oldFile))
                        System.IO.File.Delete(oldFile);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        files[0].CopyTo(fileStream);

                    productVm.Product.Picture = fileName + extension;
                }
                else
                {
                    productVm.Product.Picture = product.Picture;
                }

                _db.Products.Update(productVm.Product);
            }

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var product = _db.Products
                .Include(u => u.Category)
                .Include(u => u.ApplicationType)
                .FirstOrDefault(u => u.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var product = _db.Products.Find(id);

            if (product == null)
                return NotFound();

            var upload = _webHostEnvironment.WebRootPath + WebConstant.ImagePath;
            var oldFile = Path.Combine(upload, product.Picture);

            if (System.IO.File.Exists(oldFile)) System.IO.File.Delete(oldFile);

            _db.Products.Remove(product);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
