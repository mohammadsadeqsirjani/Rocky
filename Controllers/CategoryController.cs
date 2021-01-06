using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rocky.Data;
using Rocky.Dto.Category;
using Rocky.Models;
using System.Collections.Generic;

namespace Rocky.Controllers
{
    [Authorize(Roles = WebConstant.AdminRole)]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IValidator<CategoryAddDto> _categoryAddDtoValidator;
        private readonly IValidator<CategoryEditDto> _categoryEditDtoValidator;

        public CategoryController(ApplicationDbContext db, IMapper mapper, IValidator<CategoryAddDto> categoryAddDtoValidator, IValidator<CategoryEditDto> categoryEditDtoValidator)
        {
            _db = db;
            _mapper = mapper;
            _categoryAddDtoValidator = categoryAddDtoValidator;
            _categoryEditDtoValidator = categoryEditDtoValidator;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _db.Categories;

            var categoryDtos = _mapper.Map<IEnumerable<CategoryGetDto>>(categories);

            return View(categoryDtos);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryAddDto categoryDto)
        {
            var validationResult = _categoryAddDtoValidator.Validate(categoryDto);

            if (!validationResult.IsValid)
                return View(categoryDto);

            var category = _mapper.Map<Category>(categoryDto);

            _db.Categories.Add(category);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var category = _db.Categories.Find(id);

            if (category == null)
                return NotFound();

            var categoryDto = _mapper.Map<CategoryEditDto>(category);

            return View(categoryDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryEditDto categoryDto)
        {
            var validationResult = _categoryEditDtoValidator.Validate(categoryDto);

            if (!validationResult.IsValid)
                return View(categoryDto);

            var category = _mapper.Map<Category>(categoryDto);

            _db.Categories.Update(category);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var category = _db.Categories.Find(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var category = _db.Categories.Find(id);

            if (category == null)
                return NotFound();

            _db.Categories.Remove(category);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
