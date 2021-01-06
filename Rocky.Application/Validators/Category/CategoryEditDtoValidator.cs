using FluentValidation;
using Rocky.Application.Dtos.Category;
using Rocky.Application.Validators.Common;

namespace Rocky.Application.Validators.Category
{
    public class CategoryEditDtoValidator : EntityEditDtoValidator<CategoryEditDto>
    {
        public CategoryEditDtoValidator()
        {
            RuleFor(c => c.Name)
                .MinimumLength(2)
                .WithMessage("The minimum length of name is 2")
                .MaximumLength(256)
                .WithMessage("The maximum length of name is 256");

            RuleFor(c => c.DisplayOrder)
                .GreaterThan(0)
                .WithMessage("Display Order must be greater than 0");
        }
    }
}
