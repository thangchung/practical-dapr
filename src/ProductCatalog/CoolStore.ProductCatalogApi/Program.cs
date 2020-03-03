using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N8T.Infrastructure;
using N8T.Infrastructure.Options;
using Serilog;
using System.Threading.Tasks;
using CoolStore.ProductCatalogApi.Persistence;
using HotChocolate;
using HotChocolate.Execution.Configuration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using N8T.Domain;
using N8T.Infrastructure.Data;
using N8T.Infrastructure.ValidationModel;

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

            // https://andrewlock.net/sharing-appsettings-json-configuration-files-between-projects-in-asp-net-core/

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
                .AddMediatR(typeof(Program))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

            builder.Services.AddGraphQL(sp => Schema.Create(c =>
                {
                    c.RegisterServiceProvider(sp);
                    c.RegisterObjectTypes(typeof(Program).Assembly);
                }),
                new QueryExecutionOptions
                {
                    IncludeExceptionDetails = true,
                    TracingPreference = TracingPreference.Always
                });

            var migrationsAssembly = typeof(Program).Assembly.GetName().Name;
            var connectionString = config.GetConnectionString("default");
            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<ProductCatalogDbContext>(options =>
                {
                    options.UseSqlServer(connectionString, sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(migrationsAssembly);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 3);
                    });
                });
            builder.Services.AddScoped<IUnitOfWork>(provider => provider.GetService<ProductCatalogDbContext>());
            builder.Services.AddScoped<IDomainEventContext>(provider => provider.GetService<ProductCatalogDbContext>());

            builder.Services.AddValidators(typeof(Program).Assembly);

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

    public class ServiceOptions
    {
        public ServiceConfig GraphApi { get; set; }
        public ServiceConfig IdentityService { get; set; }
        public ServiceConfig ProductCatalogService { get; set; }
        public ServiceConfig InventoryService { get; set; }
        public ServiceConfig ShoppingCartService { get; set; }
    }
}
