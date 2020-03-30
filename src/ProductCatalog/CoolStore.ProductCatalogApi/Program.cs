using CoolStore.ProductCatalogApi.Infrastructure.Persistence;
using CoolStore.ProductCatalogApi.UserInterface.GraphQL;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using N8T.Infrastructure;
using N8T.Infrastructure.Dapr;
using N8T.Infrastructure.Options;
using Serilog;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoolStore.ProductCatalogApi
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var (builder, configBuilder) = WebApplication.CreateBuilder(args)
                .AddCustomConfiguration();

            var config = configBuilder.Build();
            var serviceOptions = config.GetOptions<ServiceOptions>("Services");

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            builder.Host
                .UseSerilog()
                //.UseCustomHost()
                ;

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
                //.AddCustomDaprClient()
                .AddCustomGrpcClient(svc =>
                {
                //     svc.AddGrpcClient<Dapr.Client.Autogen.Grpc.Dapr.DaprClient>();
                //      svc.AddGrpcClient<InventoryApi.InventoryApiClient>(o =>
                //          {
                //              o.Address = new Uri(serviceOptions.InventoryService.GrpcUri);
                //          })
                //          .AddInterceptor<ClientLoggerInterceptor>();
                 })
                ;

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

            //app.Listen(serviceOptions.ProductCatalogService.RestUri);
            await app.RunAsync();
        }
    }

    internal static class Extensions
    {
        public static IHostBuilder UseCustomHost(this IHostBuilder hostBuilder)
        {
            return hostBuilder
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel((ctx, options) =>
                    {
                        if (ctx.HostingEnvironment.IsDevelopment())
                            IdentityModelEventSource.ShowPII = true;

                        options.Limits.MinRequestBodyDataRate = null;
                        options.Listen(IPAddress.Any, EnvironmentHelper.GetHttpPort(5202));
                    });
                });
        }

        public static IServiceCollection AddCustomDaprClient(this IServiceCollection services)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            services.AddDaprClient(client =>
            {
                client.UseJsonSerializationOptions(options);
            });

            return services;
        }
    }
}
