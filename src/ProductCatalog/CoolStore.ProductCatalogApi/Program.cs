using CoolStore.ProductCatalogApi.Boundaries.GraphQL;
using CoolStore.ProductCatalogApi.Persistence;
using CoolStore.Protobuf.Inventory.V1;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using N8T.Infrastructure;
using N8T.Infrastructure.Grpc;
using N8T.Infrastructure.Options;
using Serilog;
using System;
using System.Threading.Tasks;

namespace CoolStore.ProductCatalogApi
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

            builder.Host.UseSerilog();

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
                .AddCustomDbContext<ProductCatalogDbContext>(typeof(Program).Assembly, config)
                .AddCustomGrpcClient(svc =>
                {
                    svc.AddGrpcClient<InventoryApi.InventoryApiClient>(o =>
                        {
                            o.Address = new Uri(serviceOptions.InventoryService.GrpcUri);
                        })
                        .AddInterceptor<ClientLoggerInterceptor>();
                });

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseGraphQL("/graphql");
            app.UsePlayground(new PlaygroundOptions
            {
                QueryPath = "/graphql",
                Path = "/playground",
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/playground");
                    return Task.CompletedTask;
                });
            });

            app.Listen(serviceOptions.ProductCatalogService.RestUri);
            await app.RunAsync();
        }
    }
}