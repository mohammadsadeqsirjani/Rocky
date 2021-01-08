using FluentValidation;
using Rocky.Application.Validators.Common;
using Rocky.Application.ViewModels.Dtos.InquiryHeader;

namespace Rocky.Application.Validators.InquiryHeader
{
    public class InquiryHeaderAddDtoValidator : EntityAddDtoValidator<InquiryHeaderAddDto>
    {
        public InquiryHeaderAddDtoValidator()
        {
            RuleFor(r => r.Fullname)
                .MaximumLength(100)
                .WithMessage("Fullname length must be less than or equal to 100 characters");

            RuleFor(r => r.PhoneNumber)
                .MaximumLength(20)
                .WithMessage("PhoneNumber length must be less than or equal to 20 characters");

            RuleFor(r => r.Email)
                .MaximumLength(256)
                .WithMessage("Email length must be less than or equal to 256 characters");
        }
    }
}
