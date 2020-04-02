using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using CoolStore.InventoryApi.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N8T.Infrastructure;
using N8T.Infrastructure.Data;
using N8T.Infrastructure.Options;
using N8T.Infrastructure.ValidationModel;
using Serilog;

namespace CoolStore.InventoryApi
{
    internal class Program
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private static async Task Main(string[] args)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var (builder, configBuilder) = WebApplication.CreateBuilder(args)
                .AddCustomConfiguration();

            AddTyeBindingSecrets(configBuilder);

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
                .AddControllers()
                .AddDapr();

            builder.Services
                .AddSingleton(serviceOptions)
                .AddLogging()
                .AddCustomMediatR(typeof(Program))
                .AddCustomValidators(typeof(Program).Assembly)
                .AddCustomDbContext<InventoryDbContext>(typeof(Program).Assembly, connString)
                .AddDaprClient(
                    client =>
                    {
                        client.UseJsonSerializationOptions(_options);
                    });

            var app = builder.Build();

            app
                .UseRouting()
                .UseCloudEvents()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapSubscribeHandler();
                    endpoints.MapControllers();
                });

            await app.RunAsync();
        }

        private static void AddTyeBindingSecrets(IConfigurationBuilder config)
        {
            if (Directory.Exists("/var/tye/bindings/"))
            {
                foreach (var directory in Directory.GetDirectories("/var/tye/bindings/"))
                {
                    Console.WriteLine($"Adding config in '{directory}'.");
                    config.AddKeyPerFile(directory, optional: true);
                }
            }
        }
    }
}
