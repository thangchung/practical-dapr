using System;
using System.Diagnostics;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using HotChocolate.AspNetCore.Subscriptions;
using HotChocolate.Execution;
using HotChocolate.Stitching;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using N8T.Infrastructure;
using N8T.Infrastructure.Tye;
using Serilog;

namespace CoolStore.GraphApi
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;

            var (builder, configBuilder) = WebApplication.CreateBuilder(args)
                .AddCustomConfiguration();

            var config = configBuilder.Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            builder.Host
                .UseSerilog();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddHttpClient("productCatalog",
                (sp, client) =>
                {
                    client.BaseAddress = new Uri($"{config.GetTyeAppUrl("product-catalog-api")}/graphql");
                });

            // builder.Services.AddHttpClient("inventory",
            //     (sp, client) =>
            //     {
            //         client.BaseAddress = new Uri($"{serviceOptions.InventoryService.RestUri}/graphql");
            //     });
            // builder.Services.AddHttpClient("shopping_cart",
            //     (sp, client) =>
            //     {
            //         client.BaseAddress = new Uri($"{serviceOptions.ShoppingCartService.RestUri}/graphql");
            //     });

            builder.Services.AddSingleton<IQueryResultSerializer, JsonQueryResultSerializer>();
            builder.Services
                .AddGraphQLSubscriptions()
                .AddStitchedSchema(stitchingBuilder => stitchingBuilder
                    .AddSchemaFromHttp("productCatalog")
                    //.AddSchemaFromHttp("inventory")
                    //.AddSchemaFromHttp("shopping_cart")
                );

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

            await app.RunAsync();
        }
    }
}
