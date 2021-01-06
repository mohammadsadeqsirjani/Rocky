using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rocky.Application.Dtos.ApplicationType;
using Rocky.Application.Utilities;
using Rocky.Domain.Entities;
using Rocky.Infra.Data.Persistence;
using System.Collections.Generic;

namespace Rocky.Controllers
{

    [Authorize(Roles = WebConstant.AdminRole)]
    public class ApplicationTypeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IValidator<ApplicationTypeAddDto> _applicationTypeAddDtoValidator;
        private readonly IValidator<ApplicationTypeEditDto> _applicationTypeEditDtoValidator;

        public ApplicationTypeController(ApplicationDbContext db, IMapper mapper, IValidator<ApplicationTypeAddDto> applicationTypeAddDtoValidator, IValidator<ApplicationTypeEditDto> applicationTypeEditDtoValidator)
        {
            _db = db;
            _mapper = mapper;
            _applicationTypeAddDtoValidator = applicationTypeAddDtoValidator;
            _applicationTypeEditDtoValidator = applicationTypeEditDtoValidator;
        }

        public IActionResult Index()
        {
            IEnumerable<ApplicationType> applicationTypes = _db.ApplicationTypes;

            var applicationTypeDtos = _mapper.Map<IEnumerable<ApplicationTypeGetDto>>(applicationTypes);

            return View(applicationTypeDtos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationTypeAddDto applicationTypeDto)
        {
            var validationResult = _applicationTypeAddDtoValidator.Validate(applicationTypeDto);

            if (!validationResult.IsValid)
                return View(applicationTypeDto);

            var applicationType = _mapper.Map<ApplicationType>(applicationTypeDto);

            _db.ApplicationTypes.Add(applicationType);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var applicationType = _db.ApplicationTypes.Find(id);

            var applicationTypeDto = _mapper.Map<ApplicationTypeEditDto>(applicationType);

            if (applicationType == null)
                return NotFound();

            return View(applicationTypeDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationTypeEditDto applicationTypeDto)
        {
            var validationResult = _applicationTypeEditDtoValidator.Validate(applicationTypeDto);

            if (!validationResult.IsValid)
                return View(applicationTypeDto);

            var applicationType = _mapper.Map<ApplicationType>(applicationTypeDto);

            _db.ApplicationTypes.Update(applicationType);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var applicationType = _db.ApplicationTypes.Find(id);

            if (applicationType == null)
                return NotFound();

            return View(applicationType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var applicationType = _db.ApplicationTypes.Find(id);

            if (applicationType == null)
                return NotFound();

            _db.ApplicationTypes.Remove(applicationType);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
