using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Apis.Gateways;
using CoolStore.ProductCatalogApi.Apis.GraphQL;
using CoolStore.ProductCatalogApi.Domain;
using CoolStore.ProductCatalogApi.Infrastructure.Persistence;
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

            var connString = config.GetTyeSqlServerConnString("sqlserver", "productcatalogdb");

            builder.Services
                .AddHttpContextAccessor()
                .AddCustomMediatR(typeof(Program))
                .AddCustomValidators(typeof(Program).Assembly)
                .AddCustomDbContext<ProductCatalogDbContext>(typeof(Program).Assembly, connString)
                .AddCustomGraphQL(c =>
                {
                    c.RegisterQueryType<QueryType>();
                    c.RegisterMutationType<MutationType>();
                    c.RegisterObjectTypes(typeof(Program).Assembly);
                    c.RegisterExtendedScalarTypes();
                })
                .AddScoped<IInventoryGateway, InventoryGateway>();

            var app = builder.Build();

            app.UseStaticFiles()
                .UseGraphQL("/graphql")
                .UsePlayground(new PlaygroundOptions {QueryPath = "/graphql", Path = "/playground"})
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
}
