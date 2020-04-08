using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CoolStore.WebUI.Host
{
    public class Program
    {
        // public static void Main(string[] args)
        // {
        //     BuildWebHost(args).Run();
        // }
        //
        // public static IWebHost BuildWebHost(string[] args) =>
        //     WebHost.CreateDefaultBuilder(args)
        //         .UseConfiguration(new ConfigurationBuilder()
        //             .AddCommandLine(args)
        //             .Build())
        //         .UseStartup<Startup>()
        //         .Build();

        public static void Main(string[] args)
        {
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
