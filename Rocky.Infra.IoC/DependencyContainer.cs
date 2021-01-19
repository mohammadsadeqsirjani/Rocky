using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Persistence.Initialize;
using Rocky.Infra.IoC.Extensions;

namespace Rocky.Infra.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = services.AddControllersWithViews();

            services.RegisterAutoMapper()
                .AddDistributedMemoryCache()
                .RegisterSession()
                .RegisterService(configuration)
                .RegisterValidation(builder)
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddDefaultTokenProviders().AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddExternalLoginProvider();

            return services;
        }

        public static IApplicationBuilder EnableMiddleWares(this IApplicationBuilder app, IWebHostEnvironment env, IDatabaseInitializer db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error")
                    .UseHsts();
            }

            app.UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization();

            db.Initialize();

            app.UseSession()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });

            return app;
        }
    }
}
