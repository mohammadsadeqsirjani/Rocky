using FluentValidation;
using Rocky.Application.Validators.Common;

namespace Rocky.Application.Validators.ApplicationUser
{
    public class ApplicationUserValidator : EntityValidator<Domain.Entities.ApplicationUser>
    {
        public ApplicationUserValidator()
        {
            RuleFor(a => a.FullName)
                .MaximumLength(100)
                .WithMessage("Max length of full name is 100");
            RuleFor(a => a.StreetAddress)
                .MaximumLength(100)
                .WithMessage("Max length of street address is 100");
            RuleFor(a => a.City)
                .MaximumLength(100)
                .WithMessage("Max length of city is 100");
            RuleFor(a => a.PostalCode)
                .MaximumLength(100)
                .WithMessage("Max length of posal code is 100");
        }
    }
}
