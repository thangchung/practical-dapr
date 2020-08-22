using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CoolStore.Protobuf.Inventory.V1;
using CoolStore.ShoppingCartApi.Domain;
using CoolStore.ShoppingCartApi.Infrastructure.Apis.Gateways;
using CoolStore.ShoppingCartApi.Infrastructure.Apis.GraphQL;
using CoolStore.ShoppingCartApi.Infrastructure.Persistence;
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
using N8T.Infrastructure.Validator;

Activity.DefaultIdFormat = ActivityIdFormat.W3C;
AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

var (builder, config) = WebApplication.CreateBuilder(args)
    .AddCustomConfiguration();

var appOptions = config.GetOptions<AppOptions>("app");
Console.WriteLine(Figgle.FiggleFonts.Doom.Render($"{appOptions.Name}"));

builder.Services
    .AddHttpContextAccessor()
    .AddCustomMediatR(typeof(Cart))
    .AddCustomValidators(typeof(Cart))
    .AddCustomDbContext<ShoppingCartDbContext>(
        typeof(Cart),
        config.GetConnectionString(Consts.SQLSERVER_DB_ID))
    .AddCustomGraphQL(c =>
    {
        c.RegisterQueryType<QueryType>();
        c.RegisterMutationType<MutationType>();
        c.RegisterObjectTypes(typeof(Cart));
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
