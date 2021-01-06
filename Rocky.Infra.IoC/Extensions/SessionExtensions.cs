using Microsoft.Extensions.DependencyInjection;

namespace Rocky.Infra.IoC.Extensions
{
    public static class SessionExtensions
    {
        public static IServiceCollection RegisterSession(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSession(options =>
            {
                options.IdleTimeout = 10.Minutes();
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            return services;
        }
    }
}
