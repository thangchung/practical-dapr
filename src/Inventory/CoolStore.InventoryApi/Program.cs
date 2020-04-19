using System.Diagnostics;
using System.Threading.Tasks;
using CoolStore.InventoryApi.Infrastructure.Persistence;
using CoolStore.InventoryApi.UserInterface.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using N8T.Infrastructure;
using N8T.Infrastructure.Data;
using N8T.Infrastructure.Grpc;
using N8T.Infrastructure.Tye;
using N8T.Infrastructure.ValidationModel;
using Serilog;

namespace CoolStore.InventoryApi
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;

            var (builder, configBuilder) = WebApplication.CreateBuilder(args)
                .AddCustomConfiguration();

            configBuilder.AddTyeBindingSecrets();

            var config = configBuilder.Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            builder.Host
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.ConfigureEndpointDefaults(o => o.Protocols = HttpProtocols.Http2);
                    });
                })
                .UseSerilog();

            var connString = config.GetTyeSqlServerConnString("sqlserver", "inventorydb");

            builder.Services
                .AddLogging()
                .AddCustomMediatR(typeof(Program))
                .AddCustomValidators(typeof(Program).Assembly)
                .AddCustomDbContext<InventoryDbContext>(typeof(Program).Assembly, connString)
                .AddCustomGrpc();

            var app = builder.Build();

            app
                .UseRouting()
                .UseCloudEvents()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapSubscribeHandler();
                    endpoints.MapGrpcService<DaprService>();
                });

            await app.RunAsync();
        }
    }
}
