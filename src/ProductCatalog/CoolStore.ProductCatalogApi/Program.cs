using CoolStore.ProductCatalogApi.Boundaries.GraphQL;
using CoolStore.ProductCatalogApi.Persistence;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using N8T.Domain;
using N8T.Infrastructure;
using N8T.Infrastructure.Data;
using N8T.Infrastructure.Options;
using Serilog;
using System.Threading.Tasks;

namespace CoolStore.ProductCatalogApi
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var (builder, configBuilder) = WebApplication.CreateBuilder(args)
                .AddCustomConfiguration();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            builder.Host.UseSerilog();

            var config = configBuilder.Build();
            var serviceOptions = config.GetOptions<ServiceOptions>("Services");

            builder.Services
                .AddLogging()
                .AddHttpContextAccessor()
                .AddCustomMediatR(typeof(Program))
                .AddCustomGraphQL(schemaConfiguration =>
                {
                    schemaConfiguration.RegisterQueryType<QueryType>();
                    schemaConfiguration.RegisterMutationType<MutationType>();
                })
                .AddCustomValidators(typeof(Program).Assembly)
                .AddCustomDbContext(config);

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

            app.Listen(serviceOptions.ProductCatalogService.RestUri);
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

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ProductCatalogDbContext>(options =>
                {
                    options.UseSqlServer(config.GetConnectionString("MainDb"), sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Program).Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 3);
                    });
                });
            services.AddScoped<IDbFacadeResolver>(provider => provider.GetService<ProductCatalogDbContext>());
            services.AddScoped<IDomainEventContext>(provider => provider.GetService<ProductCatalogDbContext>());
            services.AddHostedService<DbContextMigratorHostedService>();
            return services;
        }
    }
}