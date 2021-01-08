using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rocky.Application.Utilities;
using Rocky.Application.ViewModels.Dtos.Category;
using Rocky.Domain.Entities;
using Rocky.Domain.Interfaces.Category;
using System.Collections.Generic;

namespace Rocky.Controllers
{
    [Authorize(Roles = WebConstant.AdminRole)]
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;
        private readonly IValidator<CategoryAddDto> _categoryAddDtoValidator;
        private readonly IValidator<CategoryEditDto> _categoryEditDtoValidator;

        public CategoryController(IMapper mapper, IValidator<CategoryAddDto> categoryAddDtoValidator, IValidator<CategoryEditDto> categoryEditDtoValidator, ICategoryRepository repository)
        {
            _mapper = mapper;
            _categoryAddDtoValidator = categoryAddDtoValidator;
            _categoryEditDtoValidator = categoryEditDtoValidator;
            _repository = repository;
        }

        public IActionResult Index()
        {
            var categories = _repository.Select();

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
            {
                TempData[WebConstant.Failed] = WebConstant.MissionFail;
                return View(categoryDto);
            }

            var category = _mapper.Map<Category>(categoryDto);

            _repository.Add(category);

            TempData[WebConstant.Succeed] = WebConstant.MissionComplete;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var category = _repository.FirstOrDefault(id.GetValueOrDefault());

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
            {
                TempData[WebConstant.Failed] = WebConstant.MissionFail;
                return View(categoryDto);
            }

            var category = _mapper.Map<Category>(categoryDto);

            _repository.Update(category);

            TempData[WebConstant.Succeed] = WebConstant.MissionComplete;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var category = _repository.FirstOrDefault(id.GetValueOrDefault());

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var category = _repository.FirstOrDefault(id.GetValueOrDefault());

            if (category == null)
            {
                TempData[WebConstant.Failed] = WebConstant.MissionFail;
                return NotFound();
            }

            _repository.Delete(id.GetValueOrDefault());

            TempData[WebConstant.Succeed] = WebConstant.MissionComplete;

            return RedirectToAction(nameof(Index));
        }
    }
}
