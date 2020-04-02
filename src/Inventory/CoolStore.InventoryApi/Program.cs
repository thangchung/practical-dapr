using System.Threading.Tasks;
using CoolStore.InventoryApi.Infrastructure.Persistence;
using CoolStore.InventoryApi.UserInterface.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N8T.Infrastructure;
using N8T.Infrastructure.Data;
using N8T.Infrastructure.Grpc;
using N8T.Infrastructure.Options;
using N8T.Infrastructure.ValidationModel;
using Serilog;

namespace CoolStore.InventoryApi
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var (builder, configBuilder) = WebApplication.CreateBuilder(args)
                .AddCustomConfiguration();

            configBuilder.AddTyeSecrets();

            var config = configBuilder.Build();
            var serviceOptions = config.GetOptions<ServiceOptions>("Services");

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            builder.Host
                .UseSerilog();

            var connString = config["connectionstring:sqlserver"] ??
                             $"Data Source={config["service:sqlserver:host"]},{config["service:sqlserver:port"]};Initial Catalog=cs_inventory_db;User Id=sa;Password=P@ssw0rd;MultipleActiveResultSets=True;";

            builder.Services
                .AddSingleton(serviceOptions)
                .AddLogging()
                .AddCustomMediatR(typeof(Program))
                .AddCustomValidators(typeof(Program).Assembly)
                .AddCustomDbContext<InventoryDbContext>(typeof(Program).Assembly, connString)
                .AddCustomGrpc();

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
}
