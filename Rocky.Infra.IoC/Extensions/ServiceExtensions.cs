using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Rocky.Application.Utilities;
using Rocky.Application.Utilities.BrainTree;
using Rocky.Infra.Data.Scrutor;

namespace Rocky.Infra.IoC.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterService(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
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

            services.Configure<BrainTreeGateway>(configuration.GetSection("BrainTreeS"));

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddSingleton<IBrainTreeGateway, BrainTreeGateway>();


            services.Scan(i =>
            {
                i.FromApplicationDependencies()
                    .AddClasses(c => c.AssignableTo<ITransient>())
                    .AsImplementedInterfaces()
                    .WithTransientLifetime();

                i.FromApplicationDependencies()
                    .AddClasses(c => c.AssignableTo<IScoped>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();

                i.FromApplicationDependencies()
                    .AddClasses(c => c.AssignableTo<ISingleton>())
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime();
            });

            return services;
        }
    }
}
