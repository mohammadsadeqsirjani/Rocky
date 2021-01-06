using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rocky.Application.Dtos.ApplicationType;
using Rocky.Application.Dtos.Category;
using Rocky.Application.Dtos.Product;
using Rocky.Application.Mappers.Profiles;
using Rocky.Application.Utilities;
using Rocky.Data;
using Rocky.Validators.ApplicationType;
using Rocky.Validators.Category;
using Rocky.Validators.Product;

namespace Rocky
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile(new ProductProfile());
                config.AddProfile(new CategoryProfile());
                config.AddProfile(new ApplicationTypeProfile());
            });

            var mapper = mapperConfiguration.CreateMapper();

            services.AddSingleton(mapper);

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddDefaultTokenProviders().AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddDistributedMemoryCache();
            services.AddHttpContextAccessor();
            services.AddSession(options =>
            {
                options.IdleTimeout = 10.Minutes();
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var builder = services.AddControllersWithViews();

            builder.AddFluentValidation();

            services.AddTransient<IValidator<CategoryAddDto>, CategoryAddDtoValidator>();
            services.AddTransient<IValidator<CategoryEditDto>, CategoryEditDtoValidator>();
            services.AddTransient<IValidator<ApplicationTypeAddDto>, ApplicationTypeAddDtoValidator>();
            services.AddTransient<IValidator<ApplicationTypeEditDto>, ApplicationTypeEditDtoValidator>();
            services.AddTransient<IValidator<ProductUpsertDto>, ProductUpsertDtoValidator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
