using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Rocky.Areas.Identity.IdentityHostingStartup))]
namespace Rocky.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {

            });
        }
    }
}