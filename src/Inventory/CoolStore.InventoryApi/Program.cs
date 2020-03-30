using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using N8T.Infrastructure;
using N8T.Infrastructure.Options;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;
using CoolStore.InventoryApi.Infrastructure.Persistence;
using CoolStore.InventoryApi.UserInterface.Grpc;
using N8T.Infrastructure.Dapr;

namespace CoolStore.InventoryApi
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var (builder, configBuilder) = WebApplication.CreateBuilder(args)
                .AddCustomConfiguration();

            var config = configBuilder.Build();
            var serviceOptions = config.GetOptions<ServiceOptions>("Services");

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            builder.Host
                .UseSerilog()
                //.UseCustomHost()
                ;

            var connString = config["connectionstring:sqlserver"] ??
                             $"Data Source={config["service:sqlserver:host"]},{config["service:sqlserver:port"]};Initial Catalog=CoolStoreDb;User Id=sa;Password=P@ssw0rd;MultipleActiveResultSets=True;";

            builder.Services
                .AddSingleton(serviceOptions)
                .AddLogging()
                .AddCustomMediatR(typeof(Program))
                .AddCustomValidators(typeof(Program).Assembly)
                .AddCustomDbContext<InventoryDbContext>(typeof(Program).Assembly, connString)
                .AddCustomGrpc()
                ;

            var app = builder.Build();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<DaprService>();
                endpoints.MapGet("/test", context => context.Response.WriteAsync("this is test message."));
            });

            await app.RunAsync();
        }
    }

    internal static class Extensions
    {
        public static IHostBuilder UseCustomHost(this IHostBuilder hostBuilder)
        {
            return hostBuilder
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel((ctx, options) =>
                    {
                        options.ConfigureEndpointDefaults(o => o.Protocols = HttpProtocols.Http2);

                        // if (ctx.HostingEnvironment.IsDevelopment())
                        //     IdentityModelEventSource.ShowPII = true;
                        //
                        // options.Limits.MinRequestBodyDataRate = null;
                        // options.Listen(IPAddress.Any, EnvironmentHelper.GetHttpPort());
                        // options.Listen(IPAddress.Any, EnvironmentHelper.GetGrpcPort(),
                        //     listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; });
                    });
                });
        }
    }
}
