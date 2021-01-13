using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Rocky.Application.Utilities;
using Rocky.Infra.Data.Scrutor;

namespace Rocky.Infra.IoC.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            //services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            //services.AddScoped(typeof(IAsyncRepository<,>), typeof(AsyncRepository<,>));

            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<ICategoryAsyncRepository, CategoryAsyncRepository>();

            //services.AddScoped<IApplicationTypeRepository, ApplicationTypeRepository>();
            //services.AddScoped<IApplicationTypeAsyncRepository, ApplicationTypeAsyncRepository>();

            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<IProductAsyncRepository, ProductAsyncRepository>();

            //services.AddScoped<IInquiryHeaderRepository, InquiryHeaderRepository>();
            //services.AddScoped<IInquiryHeaderAsyncRepository, InquiryHeaderAsyncRepository>();

            //services.AddScoped<IInquiryDetailRepository, InquiryDetailRepository>();
            //services.AddScoped<IInquiryDetailAsyncRepository, InquiryDetailAsyncRepository>();

            //services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            //services.AddScoped<IApplicationUserAsyncRepository, ApplicationUserAsyncRepository>();

            services.AddTransient<IEmailSender, EmailSender>();

            services.Scan(i =>
            {
                i.FromApplicationDependencies()
                    .AddClasses(c => c.AssignableTo<ITransient>())
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                    
                    .AddClasses(c => c.AssignableTo<IScoped>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                    
                    .AddClasses(c => c.AssignableTo<ISingleton>())
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime();
            });

            return services;
        }
    }
}
