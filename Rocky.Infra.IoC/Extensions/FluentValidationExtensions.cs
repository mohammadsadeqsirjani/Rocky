using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Rocky.Application.Dtos.ApplicationType;
using Rocky.Application.Dtos.Category;
using Rocky.Application.Dtos.Product;
using Rocky.Application.Validators.ApplicationType;
using Rocky.Application.Validators.Category;
using Rocky.Application.Validators.Product;

namespace Rocky.Infra.IoC.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IServiceCollection RegisterValidation(this IServiceCollection services, IMvcBuilder builder)
        {
            builder.AddFluentValidation();

            services.AddTransient<IValidator<CategoryAddDto>, CategoryAddDtoValidator>();
            services.AddTransient<IValidator<CategoryEditDto>, CategoryEditDtoValidator>();
            services.AddTransient<IValidator<ApplicationTypeAddDto>, ApplicationTypeAddDtoValidator>();
            services.AddTransient<IValidator<ApplicationTypeEditDto>, ApplicationTypeEditDtoValidator>();
            services.AddTransient<IValidator<ProductUpsertDto>, ProductUpsertDtoValidator>();

            return services;
        }
    }
}
