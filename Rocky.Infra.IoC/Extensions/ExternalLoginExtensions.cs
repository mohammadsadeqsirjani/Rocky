using Microsoft.Extensions.DependencyInjection;

namespace Rocky.Infra.IoC.Extensions
{
    public static class ExternalLoginExtensions
    {
        public static IServiceCollection AddExternalLoginProvider(this IServiceCollection services)
        {
            services.AddAuthentication()
                .AddFacebook(options =>
                {
                    options.AppId = "979166549209276";
                    options.AppSecret = "0136648c5c6de648ac4d61b2d6534281";
                })
                .AddGoogle(options =>
                {
                    options.ClientId = "876686244813-de8nbli652ih3s37aiob1c0567m472c1.apps.googleusercontent.com";
                    options.ClientSecret = "LCmkcKq7RCOWHABkICfwXuBx";
                });

            return services;
        }
    }
}
