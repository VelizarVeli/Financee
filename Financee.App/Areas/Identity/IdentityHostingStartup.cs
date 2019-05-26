using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Financee.App.Areas.Identity.IdentityHostingStartup))]
namespace Financee.App.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}