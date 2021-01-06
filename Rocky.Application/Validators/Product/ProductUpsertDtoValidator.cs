using FluentValidation;
using Rocky.Application.Validators.Common;
using Rocky.Application.ViewModels.Dtos.Product;

namespace Rocky.Application.Validators.Product
{
    public class ProductUpsertDtoValidator : EntityEditDtoValidator<ProductUpsertDto>
    {
        public ProductUpsertDtoValidator()
        {
            RuleFor(p => p.Name)
                .MinimumLength(2)
                .WithMessage("Name length nust be greater than or equal to 2")
                .MaximumLength(256)
                .WithMessage("Name length must be less than or equal to 256");

            RuleFor(p => p.ShortDescription)
                .MaximumLength(256)
                .WithMessage("Short Description length must be less than or equal to 256");

            RuleFor(p => p.Description)
                .MaximumLength(1000)
                .WithMessage("Description length must be less than or equal to 1000");

            RuleFor(p => p.Picture)
                .MaximumLength(256)
                .WithMessage("Picture length must be less than or equal to 256");

            RuleFor(p => p.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0");
        }
    }
}
