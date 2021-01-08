using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rocky.Application.Utilities;
using Rocky.Application.ViewModels;
using Rocky.Application.ViewModels.Dtos.Product;
using Rocky.Domain.Entities;
using Rocky.Domain.Interfaces.ApplicationType;
using Rocky.Domain.Interfaces.Category;
using Rocky.Domain.Interfaces.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rocky.Controllers
{
    [Authorize(Roles = WebConstant.AdminRole)]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IApplicationTypeRepository _applicationTypeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;
        private readonly IValidator<ProductUpsertDto> _productUpsertDtoValidator;

        public ProductController(IWebHostEnvironment webHostEnvironment, IMapper mapper, IValidator<ProductUpsertDto> productUpsertDtoValidator, IProductRepository productRepository, ICategoryRepository categoryRepository, IApplicationTypeRepository applicationTypeRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _productUpsertDtoValidator = productUpsertDtoValidator;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _applicationTypeRepository = applicationTypeRepository;
        }

        public IActionResult Index()
        {
            var products = _productRepository.Select(p => p.Category, p => p.ApplicationType);

            var productDtos = _mapper.Map<IEnumerable<ProductGetDto>>(products);

            return View(productDtos);
        }

        public IActionResult Upsert(int? id)
        {
            var productVm = new ProductVm
            {
                Product = new ProductUpsertDto(),
                Categories = _categoryRepository.Select()
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }),
                ApplicationTypes = _applicationTypeRepository.Select()
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    })
            };

            if (id == null)
            {
                TempData[WebConstant.Failed] = WebConstant.MissionFail;
                return View(productVm);
            }

            var product = _productRepository.FirstOrDefault(id.GetValueOrDefault());

            productVm.Product = _mapper.Map<ProductUpsertDto>(product);

            if (productVm.Product != null) return View(productVm);

            TempData[WebConstant.Failed] = WebConstant.MissionFail;
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVm productVm)
        {
            var validationResult = _productUpsertDtoValidator.Validate(productVm.Product);

            if (!validationResult.IsValid)
            {
                productVm.Categories = _categoryRepository.Select()
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });

                productVm.ApplicationTypes = _applicationTypeRepository.Select()
                    .Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    });

                TempData[WebConstant.Failed] = WebConstant.MissionFail;

                return View(productVm);
            }

            var files = HttpContext.Request.Form.Files;
            var webRootPath = _webHostEnvironment.WebRootPath;
            var productMapped = _mapper.Map<Product>(productVm.Product);

            if (productVm.Product.Id == 0)
            {
                var upload = webRootPath + WebConstant.ImagePath;
                var fileName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    files[0].CopyTo(fileStream);

                productMapped.Picture = fileName + extension;


                _productRepository.Add(productMapped, false);
            }
            else
            {
                var product = _productRepository.FirstOrDefault(u => u.Id == productVm.Product.Id, false);

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

                    productMapped.Picture = fileName + extension;
                }
                else
                {
                    productMapped.Picture = product.Picture;
                }

                _productRepository.Update(productMapped, false);
            }

            _productRepository.SaveChanges();
            TempData[WebConstant.Succeed] = WebConstant.MissionComplete;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData[WebConstant.Failed] = WebConstant.MissionFail;
                return NotFound();
            }

            var product = _productRepository.FirstOrDefault(u => u.Id == id.GetValueOrDefault(), p => p.Category,
                p => p.ApplicationType);

            if (product == null)
            {
                TempData[WebConstant.Failed] = WebConstant.MissionFail;
                return NotFound();
            }

            var productDto = _mapper.Map<ProductGetDto>(product);

            return View(productDto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var product = _productRepository.FirstOrDefault(id.GetValueOrDefault());

            if (product == null)
            {
                TempData[WebConstant.Failed] = WebConstant.MissionFail;
                return NotFound();
            }

            var upload = _webHostEnvironment.WebRootPath + WebConstant.ImagePath;
            var oldFile = Path.Combine(upload, product.Picture);

            if (System.IO.File.Exists(oldFile))
                System.IO.File.Delete(oldFile);

            _productRepository.Delete(product);
            TempData[WebConstant.Succeed] = WebConstant.MissionComplete;

            return RedirectToAction(nameof(Index));
        }
    }
}
