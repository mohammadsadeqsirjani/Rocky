using FluentValidation;
using Rocky.Application.Validators.Common;
using Rocky.Application.ViewModels;

namespace Rocky.Application.Validators.ShoppingCart
{
    public class DetailVmValidator : EntityValidator<DetailsVm>
    {
        public DetailVmValidator()
        {
            RuleFor(r => r.SqFt)
                .GreaterThan(0)
                .WithMessage("SqFr must be greater than 0")
                .LessThan(10000)
                .WithMessage("SqFt must be less than 10000");
        }
    }
}
