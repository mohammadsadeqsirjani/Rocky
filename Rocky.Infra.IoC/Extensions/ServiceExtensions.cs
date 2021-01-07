using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Rocky.Application.Utilities;
using Rocky.Domain.Interfaces.ApplicationType;
using Rocky.Domain.Interfaces.Category;
using Rocky.Domain.Interfaces.Common;
using Rocky.Domain.Interfaces.Product;
using Rocky.Infra.Data.Repositories.ApplicationType;
using Rocky.Infra.Data.Repositories.Category;
using Rocky.Infra.Data.Repositories.Common;
using Rocky.Infra.Data.Repositories.Product;

namespace Rocky.Infra.IoC.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryAsyncRepository, CategoryAsyncRepository>();

            services.AddScoped<IApplicationTypeRepository, ApplicationTypeRepository>();
            services.AddScoped<IApplicationTypeAsyncRepository, ApplicationTypeAsyncRepository>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAsyncRepository, ProductAsyncRepository>();

            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}
