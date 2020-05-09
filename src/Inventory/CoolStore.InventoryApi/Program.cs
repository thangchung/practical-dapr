using System.Diagnostics;
using System.Threading.Tasks;
using CoolStore.InventoryApi.Infrastructure.Apis.GraphQL;
using CoolStore.InventoryApi.Infrastructure.Apis.Grpc;
using CoolStore.InventoryApi.Infrastructure.Persistence;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using N8T.Infrastructure;
using N8T.Infrastructure.Dapr;
using N8T.Infrastructure.Data;
using N8T.Infrastructure.GraphQL;
using N8T.Infrastructure.Grpc;
using N8T.Infrastructure.Kestrel;
using N8T.Infrastructure.ValidationModel;

namespace CoolStore.InventoryApi
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;

            var (builder, config) = WebApplication.CreateBuilder(args)
                .AddCustomConfiguration();

            builder.Host
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options =>
                        options.ListenHttpAndGrpcProtocols(config, Consts.INVENTORY_API_ID));
                });

            builder.Services
                .AddHttpContextAccessor()
                .AddCustomMediatR(typeof(Program))
                .AddCustomValidators(typeof(Program))
                .AddCustomDbContext<InventoryDbContext>(typeof(Program), config.GetConnectionString(Consts.SQLSERVER_DB_ID))
                .AddCustomMvc(typeof(Program), withDapr: true)
                .AddCustomGraphQL(c =>
                {
                    c.RegisterQueryType<QueryType>();
                    c.RegisterObjectTypes(typeof(Program));
                    c.RegisterExtendedScalarTypes();
                })
                .AddCustomGrpc()
                .AddCustomDaprClient();

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
                    endpoints.MapGrpcService<InventoryService>();
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
