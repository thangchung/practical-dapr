using System;
using CoolStore.InventoryApi.Boundaries.Grpc;
using CoolStore.InventoryApi.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using N8T.Infrastructure;
using N8T.Infrastructure.Options;
using Serilog;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
                .UseCustomHost();

            builder.Services
                .AddSingleton(serviceOptions)
                .AddLogging()
                .AddCustomMediatR(typeof(Program))
                .AddCustomValidators(typeof(Program).Assembly)
                .AddCustomDbContext<InventoryDbContext>(typeof(Program).Assembly, config)
                .AddCustomGrpc();

            var app = builder.Build();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<InventoryService>();
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
                        if (ctx.HostingEnvironment.IsDevelopment())
                            IdentityModelEventSource.ShowPII = true;

                        options.Limits.MinRequestBodyDataRate = null;
                        options.Listen(IPAddress.Any,
                            Environment.GetEnvironmentVariable("DAPR_HTTP_PORT").ConvertTo<int>());
                        options.Listen(IPAddress.Any,
                            Environment.GetEnvironmentVariable("DAPR_GRPC_PORT").ConvertTo<int>(),
                            listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; });
                    });
                });
        }
    }
}