﻿using FluentValidation;
using Rocky.Dto.Category;
using Rocky.Validators.Common;

namespace Rocky.Validators.Category
{
    public class CategoryAddDtoValidator : EntityAddDtoValidator<CategoryAddDto>
    {
        public CategoryAddDtoValidator()
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