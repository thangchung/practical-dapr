using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CoolStore.WebUI.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;

            var host = CreateHostBuilder(args).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
