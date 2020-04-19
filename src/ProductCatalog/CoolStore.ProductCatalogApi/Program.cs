using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Domain;
using CoolStore.ProductCatalogApi.Infrastructure.Persistence;
using CoolStore.ProductCatalogApi.UserInterface.Gateways;
using CoolStore.ProductCatalogApi.UserInterface.GraphQL;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using N8T.Infrastructure;
using N8T.Infrastructure.Data;
using N8T.Infrastructure.GraphQL;
using N8T.Infrastructure.Tye;
using N8T.Infrastructure.ValidationModel;
using Serilog;

namespace CoolStore.ProductCatalogApi
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var (builder, configBuilder) = WebApplication.CreateBuilder(args)
                .AddCustomConfiguration();

            configBuilder.AddTyeBindingSecrets();

            var config = configBuilder.Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            builder.Host
                .UseSerilog();

            var connString = config.GetTyeSqlServerConnString("sqlserver", "productcatalogdb");

            builder.Services
                .AddLogging()
                .AddHttpContextAccessor()
                .AddCustomMediatR(typeof(Program))
                .AddCustomGraphQL(c =>
                {
                    c.RegisterQueryType<QueryType>();
                    c.RegisterMutationType<MutationType>();
                    c.RegisterObjectTypes(typeof(Program).Assembly);
                    c.RegisterExtendedScalarTypes();
                })
                .AddCustomValidators(typeof(Program).Assembly)
                .AddCustomDbContext<ProductCatalogDbContext>(typeof(Program).Assembly, connString)
                .AddCustomServices();

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
    }

    internal static class Extensions
    {
        internal static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IInventoryGateway, InventoryGateway>();
            return services;
        }
    }
}
