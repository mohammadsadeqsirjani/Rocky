using FluentValidation;
using Rocky.Application.Validators.Common;
using Rocky.Application.ViewModels.Dtos.ApplicationType;

namespace Rocky.Application.Validators.ApplicationType
{
    public class ApplicationTypeAddDtoValidator : EntityAddDtoValidator<ApplicationTypeAddDto>
    {
        public ApplicationTypeAddDtoValidator()
        {
            RuleFor(a => a.Name)
                .MinimumLength(2)
                .WithMessage("Application Type Name length must be greate or equal to 2")
                .MaximumLength(256)
                .WithMessage("Application Type Name length must be less than or equal to 256");
        }
    }
}
