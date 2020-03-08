using CoolStore.InventoryApi.Boundaries.Grpc;
using CoolStore.InventoryApi.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using N8T.Domain;
using N8T.Infrastructure;
using N8T.Infrastructure.Data;
using N8T.Infrastructure.Options;
using Serilog;
using System.Net;
using System.Threading.Tasks;

namespace CoolStore.InventoryApi
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

            builder.Host
                .UseSerilog()
                .UseCustomHost(serviceOptions);

            builder.Services
                .AddSingleton(serviceOptions)
                .AddLogging()
                .AddCustomMediatR(typeof(Program))
                .AddCustomValidators(typeof(Program).Assembly)
                .AddCustomDbContext(config)
                .AddCustomGrpc();

            var app = builder.Build();

            app.MapGrpcService<InventoryService>();

            app.Listen(serviceOptions.InventoryService.GrpcUri);
            await app.RunAsync();
        }
    }

    internal static class Extensions
    {
        public static (WebApplicationBuilder, IConfigurationBuilder) AddCustomConfiguration(
            this WebApplicationBuilder builder)
        {
            var configBuilder = builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

            var env = builder.Environment;
            configBuilder.AddJsonFile("services.json", optional: true);

            if (!env.IsDevelopment()) return (builder, configBuilder);

            var servicesJson = System.IO.Path.Combine(env.ContentRootPath, "..", "..", "..", "services.json");
            configBuilder.AddJsonFile(servicesJson, optional: true);

            return (builder, configBuilder);
        }

        public static IHostBuilder UseCustomHost(this IHostBuilder hostBuilder, ServiceOptions serviceOptions)
        {
            return hostBuilder
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel((ctx, options) =>
                    {
                        if (ctx.HostingEnvironment.IsDevelopment())
                        {
                            IdentityModelEventSource.ShowPII = true;
                        }

                        options.Limits.MinRequestBodyDataRate = null;
                        options.Listen(IPAddress.Any, serviceOptions.InventoryService.RestUri.GetPortOfUrl());
                        options.Listen(IPAddress.Any, serviceOptions.InventoryService.GrpcUri.GetPortOfUrl(),
                            listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; });
                    });
                });
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDbContext<InventoryDbContext>(options =>
                {
                    options.UseSqlServer(config.GetConnectionString("MainDb"), sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Program).Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 3);
                    });
                });

            services.AddScoped<IDbFacadeResolver>(provider => provider.GetService<InventoryDbContext>());
            services.AddScoped<IDomainEventContext>(provider => provider.GetService<InventoryDbContext>());
            services.AddHostedService<DbContextMigratorHostedService>();

            return services;
        }
    }
}