using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Infrastructure.Persistence;
using CoolStore.ProductCatalogApi.UserInterface.GraphQL;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N8T.Infrastructure;
using N8T.Infrastructure.Data;
using N8T.Infrastructure.GraphQL;
using N8T.Infrastructure.Options;
using N8T.Infrastructure.ValidationModel;
using Serilog;

namespace CoolStore.ProductCatalogApi
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
                             $"Data Source={config["service:sqlserver:host"]},{config["service:sqlserver:port"]};Initial Catalog=cs_product_catalog_db;User Id=sa;Password=P@ssw0rd;MultipleActiveResultSets=True;";

            builder.Services
                .AddSingleton(serviceOptions)
                .AddLogging()
                .AddHttpContextAccessor()
                .AddCustomMediatR(typeof(Program))
                .AddCustomGraphQL(schemaConfiguration =>
                {
                    schemaConfiguration.RegisterQueryType<QueryType>();
                    schemaConfiguration.RegisterMutationType<MutationType>();
                })
                .AddCustomValidators(typeof(Program).Assembly)
                .AddCustomDbContext<ProductCatalogDbContext>(typeof(Program).Assembly, connString)
                .AddDaprClient(client =>
                {
                    client.UseJsonSerializationOptions(_options);
                });

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseGraphQL("/graphql");
            app.UsePlayground(new PlaygroundOptions {QueryPath = "/graphql", Path = "/playground",});

            app
                .UseRouting()
                .UseCloudEvents()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapGet("/", context =>
                    {
                        context.Response.Redirect("/playground");
                        return Task.CompletedTask;
                    });
                    endpoints.MapSubscribeHandler();
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
