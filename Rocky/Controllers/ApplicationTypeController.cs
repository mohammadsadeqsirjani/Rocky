using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rocky.Application.Utilities;
using Rocky.Application.ViewModels.Dtos.ApplicationType;
using Rocky.Domain.Entities;
using Rocky.Domain.Interfaces.ApplicationType;
using System.Collections.Generic;

namespace Rocky.Controllers
{

    [Authorize(Roles = WebConstant.AdminRole)]
    public class ApplicationTypeController : Controller
    {
        private readonly IApplicationTypeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<ApplicationTypeAddDto> _applicationTypeAddDtoValidator;
        private readonly IValidator<ApplicationTypeEditDto> _applicationTypeEditDtoValidator;

        public ApplicationTypeController(IMapper mapper, IValidator<ApplicationTypeAddDto> applicationTypeAddDtoValidator, IValidator<ApplicationTypeEditDto> applicationTypeEditDtoValidator, IApplicationTypeRepository repository)
        {
            _mapper = mapper;
            _applicationTypeAddDtoValidator = applicationTypeAddDtoValidator;
            _applicationTypeEditDtoValidator = applicationTypeEditDtoValidator;
            _repository = repository;
        }

        public IActionResult Index()
        {
            var applicationTypes = _repository.Select();

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
            {
                TempData[WebConstant.Failed] = WebConstant.MissionFail;
                return View(applicationTypeDto);
            }

            var applicationType = _mapper.Map<ApplicationType>(applicationTypeDto);

            _repository.Add(applicationType);
            TempData[WebConstant.Succeed] = WebConstant.MissionComplete;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                TempData[WebConstant.Failed] = WebConstant.MissionFail;
                return NotFound();
            }

            var applicationType = _repository.FirstOrDefault(id.GetValueOrDefault());

            var applicationTypeDto = _mapper.Map<ApplicationTypeEditDto>(applicationType);

            if (applicationType != null) return View(applicationTypeDto);

            TempData[WebConstant.Failed] = WebConstant.MissionFail;

            return NotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationTypeEditDto applicationTypeDto)
        {
            var validationResult = _applicationTypeEditDtoValidator.Validate(applicationTypeDto);

            if (!validationResult.IsValid)
            {
                TempData[WebConstant.Failed] = WebConstant.MissionFail;
                return View(applicationTypeDto);
            }

            var applicationType = _mapper.Map<ApplicationType>(applicationTypeDto);

            _repository.Update(applicationType);
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

            var applicationType = _repository.FirstOrDefault(id.GetValueOrDefault());

            if (applicationType != null) return View(applicationType);
            TempData[WebConstant.Failed] = WebConstant.MissionFail;
            return NotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var applicationType = _repository.FirstOrDefault(id.GetValueOrDefault());

            if (applicationType == null)
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
