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
using N8T.Infrastructure.Options;
using Serilog;
using System.Threading.Tasks;
using N8T.Infrastructure.Data;

namespace CoolStore.ProductCatalogApi
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            var configurationBuilder = builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

            var env = builder.Environment;
            configurationBuilder.AddJsonFile("services.json", optional: true);
            if (env.IsDevelopment())
            {
                var servicesJson = System.IO.Path.Combine(env.ContentRootPath, "..", "..", "..", "services.json");
                configurationBuilder.AddJsonFile(servicesJson, optional: true);
            }

            var config = configurationBuilder.Build();

            builder.Host.UseSerilog();

            var serviceOptions = config.GetOptions<ServiceOptions>("Services");
            builder.Services
                .AddLogging()
                .AddHttpContextAccessor()
                .AddCustomMediatR(typeof(Program))
                .AddCustomGraphQL(typeof(Program))
                .AddCustomValidators(typeof(Program).Assembly);

            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<ProductCatalogDbContext>(options =>
                {
                    options.UseSqlServer(config.GetConnectionString("MainDb"), sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Program).Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 3);
                    });
                });
            builder.Services.AddScoped<DbContext>(provider => provider.GetService<ProductCatalogDbContext>());
            builder.Services.AddScoped<IDomainEventContext>(provider => provider.GetService<ProductCatalogDbContext>());
            builder.Services.AddHostedService<DbContextMigratorHostedService>();

            var app = builder.Build();
            app.Listen(serviceOptions.ProductCatalogService.RestUri);

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