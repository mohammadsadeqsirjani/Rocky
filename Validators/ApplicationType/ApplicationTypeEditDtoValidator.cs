using FluentValidation;
using Rocky.Dto.ApplicationType;
using Rocky.Validators.Common;

namespace Rocky.Validators.ApplicationType
{
    public class ApplicationTypeEditDtoValidator : EntityEditDtoValidator<ApplicationTypeEditDto>
    {
        public ApplicationTypeEditDtoValidator()
        {
            RuleFor(a => a.Name)
                .MinimumLength(2)
                .WithMessage("Application Type Name length must be greate or equal to 2")
                .MaximumLength(256)
                .WithMessage("Application Type Name length must be less than or equal to 256");
        }
    }
}
