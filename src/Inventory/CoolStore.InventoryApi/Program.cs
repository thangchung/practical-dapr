using System.Diagnostics;
using System.Threading.Tasks;
using CoolStore.InventoryApi.Apis.Grpc;
using CoolStore.InventoryApi.Infrastructure.Persistence;
using CoolStore.ProductCatalogApi.Apis.GraphQL;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using N8T.Infrastructure;
using N8T.Infrastructure.Data;
using N8T.Infrastructure.GraphQL;
using N8T.Infrastructure.Grpc;
using N8T.Infrastructure.Kestrel;
using N8T.Infrastructure.Tye;
using N8T.Infrastructure.ValidationModel;

namespace CoolStore.InventoryApi
{
    internal class Program
    {
        public const string INVENTORY_API_ID = "inventory-api";

        private static async Task Main(string[] args)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;

            var (builder, configBuilder) = WebApplication.CreateBuilder(args)
                .AddCustomConfiguration();

            configBuilder.AddTyeBindingSecrets();

            var config = configBuilder.Build();

            builder.Host
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                        options.ListenHttpAndGrpcProtocols(config, INVENTORY_API_ID));
                });

            var connString = config.GetTyeSqlServerConnString("sqlserver", "inventorydb");

            builder.Services
                .AddHttpContextAccessor()
                .AddCustomMediatR(typeof(Program))
                .AddCustomValidators(typeof(Program).Assembly)
                .AddCustomDbContext<InventoryDbContext>(typeof(Program).Assembly, connString)
                .AddCustomGraphQL(c =>
                {
                    c.RegisterQueryType<QueryType>();
                    c.RegisterObjectTypes(typeof(Program).Assembly);
                    c.RegisterExtendedScalarTypes();
                })
                .AddCustomGrpc()
                .AddControllers();

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
                    endpoints.MapGrpcService<DaprService>();
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
