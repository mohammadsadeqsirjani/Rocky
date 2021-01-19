using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Rocky
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            //using (var scope = host.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    var context = services.GetService<ApplicationDbContext>();

            //    new ApplicationTypeConfiguration().Seed(context);
            //    new CategoryConfiguration().Seed(context);
            //    new ProductConfiguration().Seed(context);
            //}

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
