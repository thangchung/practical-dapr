using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Apis.Gateways;
using CoolStore.ProductCatalogApi.Apis.GraphQL;
using CoolStore.ProductCatalogApi.Domain;
using CoolStore.ProductCatalogApi.Infrastructure.Persistence;
using CoolStore.Protobuf.Inventory.V1;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N8T.Infrastructure;
using N8T.Infrastructure.Auth;
using N8T.Infrastructure.Dapr;
using N8T.Infrastructure.Data;
using N8T.Infrastructure.GraphQL;
using N8T.Infrastructure.Grpc;
using N8T.Infrastructure.OTel;
using N8T.Infrastructure.Tye;
using N8T.Infrastructure.Validator;

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
                .AddCustomMediatR(typeof(Product))
                .AddCustomValidators(typeof(Product))
                .AddCustomDbContext<ProductCatalogDbContext>(typeof(Product), config.GetConnectionString(Consts.SQLSERVER_DB_ID))
                .AddCustomGraphQL<QueryType, MutationType>(c =>
                {
                    c.RegisterObjectTypes(typeof(Product));
                })
                .AddCustomGrpcClient(svc =>
                {
                    svc.AddGrpcClient<InventoryApi.InventoryApiClient>(o =>
                    {
                        o.Address = config.GetGrpcUriFor(Consts.INVENTORY_API_ID);
                    });
                })
                .AddCustomDaprClient()
                .AddCustomAuth(config,
                    services => services.Scan(s => s
                            .FromAssemblyOf<Product>()
                                .AddClasses(c => c
                                    .AssignableTo<IAuthorizationHandler>()).As<IAuthorizationHandler>()
                                .AddClasses(c=>c
                                    .AssignableTo<IAuthorizationRequirement>()).As<IAuthorizationRequirement>()
                        ),

                        //.AddScoped<IAuthorizationHandler, GetProductsAuthzHandler>()
                        //.AddScoped<IAuthorizationRequirement, GetProductsQuery>()
                        //.AddScoped<IAuthorizationHandler, GetCategoriesAuthzHandler>()
                        //.AddScoped<IAuthorizationRequirement, GetCategoriesQuery>(),
                    options => options.Authority = config.GetServiceUri("identityserver")?.Authority)
                .AddCustomOtelWithZipkin(config, o => o.Endpoint = new Uri($"http://{config.GetServiceUri("zipkin")?.DnsSafeHost}:9411/api/v2/spans"))
                .AddScoped<IInventoryGateway, InventoryGateway>();

            var app = builder.Build();

            app.UseStaticFiles()
                .UseGraphQL("/graphql")
                .UsePlayground(new PlaygroundOptions {QueryPath = "/graphql", Path = "/playground"})
                .UseRouting()
                .UseCloudEvents()
                .UseAuthentication()
                .UseAuthorization()
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
}
