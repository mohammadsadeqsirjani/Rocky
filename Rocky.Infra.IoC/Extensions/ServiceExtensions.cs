using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Rocky.Application.Utilities;
using Rocky.Domain.Interfaces.Category;
using Rocky.Domain.Interfaces.Common;
using Rocky.Infra.Data.Repositories.Category;
using Rocky.Infra.Data.Repositories.Common;

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

            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}
