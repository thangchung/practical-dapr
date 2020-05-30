using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Apis.Gateways;
using CoolStore.ProductCatalogApi.Apis.GraphQL;
using CoolStore.ProductCatalogApi.Domain;
using CoolStore.ProductCatalogApi.Infrastructure.Persistence;
using CoolStore.Protobuf.Inventory.V1;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N8T.Infrastructure;
using N8T.Infrastructure.Dapr;
using N8T.Infrastructure.Data;
using N8T.Infrastructure.GraphQL;
using N8T.Infrastructure.Grpc;
using N8T.Infrastructure.Tye;
using N8T.Infrastructure.ValidationModel;

namespace CoolStore.ProductCatalogApi
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var (builder, config) = WebApplication.CreateBuilder(args)
                .AddCustomConfiguration();

            var appOptions = config.GetOptions<AppOptions>("app");
            Console.WriteLine(Figgle.FiggleFonts.Doom.Render($"{appOptions.Name}"));

            builder.Services
                .AddHttpContextAccessor()
                .AddCustomMediatR(typeof(Program))
                .AddCustomValidators(typeof(Program))
                .AddCustomDbContext<ProductCatalogDbContext>(
                    typeof(Program),
                    config.GetConnectionString(Consts.SQLSERVER_DB_ID))
                .AddCustomMvc(typeof(Program), withDapr: false)
                .AddCustomGraphQL(c =>
                {
                    c.RegisterQueryType<QueryType>();
                    c.RegisterMutationType<MutationType>();
                    c.RegisterObjectTypes(typeof(Program));
                    c.RegisterExtendedScalarTypes();
                })
                .AddCustomGrpcClient(svc =>
                {
                    svc.AddGrpcClient<InventoryApi.InventoryApiClient>(o =>
                    {
                        o.Address = config.GetGrpcUriFor(Consts.INVENTORY_API_ID);
                    });
                })
                .AddCustomDaprClient()
                .AddScoped<IStoreGateway, StoreGateway>();

            var app = builder.Build();

            app.UseStaticFiles()
                .UseGraphQL("/graphql")
                .UsePlayground(new PlaygroundOptions {QueryPath = "/graphql", Path = "/playground"})
                .UseRouting()
                .UseCloudEvents()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapSubscribeHandler();
                    endpoints.MapGet("/", context =>
                    {
                        context.Response.Redirect("/playground");
                        return Task.CompletedTask;
                    });
                });

            await app.RunAsync();
        }
    }
}
