using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Rocky.Application.Utilities;

namespace Rocky.Infra.IoC.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }
    }
}
