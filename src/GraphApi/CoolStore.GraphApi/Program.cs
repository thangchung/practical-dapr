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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N8T.Infrastructure;
using N8T.Infrastructure.Tye;
using OpenTelemetry.Trace;
using OpenTelemetry.Trace.Samplers;

Activity.DefaultIdFormat = ActivityIdFormat.W3C;

var (builder, config) = WebApplication.CreateBuilder(args)
    .AddCustomConfiguration();

var appOptions = config.GetOptions<AppOptions>("app");
Console.WriteLine(Figgle.FiggleFonts.Doom.Render($"{appOptions.Name}"));

builder.Services.AddHttpClient(Consts.PRODUCT_CATALOG_GRAPHQL_CLIENT,
    (sp, client) =>
    {
        client.BaseAddress = config.GetGraphQLUriFor(Consts.PRODUCT_CATALOG_API_ID);
    });

builder.Services.AddHttpClient(Consts.INVENTORY_GRAPHQL_CLIENT,
    (sp, client) =>
    {
        client.BaseAddress = config.GetGraphQLUriFor(Consts.INVENTORY_API_ID);
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IQueryResultSerializer, JsonQueryResultSerializer>();
builder.Services
    .AddGraphQLSubscriptions()
    .AddStitchedSchema(stitchingBuilder => stitchingBuilder
        .AddSchemaFromHttp(Consts.PRODUCT_CATALOG_GRAPHQL_CLIENT)
        .AddSchemaFromHttp(Consts.INVENTORY_GRAPHQL_CLIENT)
    )
    .AddOpenTelemetry(b => b
        .SetSampler(new AlwaysOnSampler())
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .UseZipkinExporter(o =>
        {
            o.ServiceName = "graph-api";
            o.Endpoint = new Uri($"http://{config.GetServiceUri("zipkin")?.DnsSafeHost}:9411/api/v2/spans");
        })
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
