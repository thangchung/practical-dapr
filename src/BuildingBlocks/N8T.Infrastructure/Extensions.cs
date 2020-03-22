using HotChocolate;
using HotChocolate.Configuration;
using HotChocolate.Execution.Configuration;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N8T.Infrastructure.Data;
using N8T.Infrastructure.GraphQL.Errors;
using N8T.Infrastructure.Grpc;
using N8T.Infrastructure.Logging;
using N8T.Infrastructure.ValidationModel;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using N8T.Domain;
using Path = System.IO.Path;

namespace N8T.Infrastructure
{
    public static class Extensions
    {
        public static (WebApplicationBuilder, IConfigurationBuilder) AddCustomConfiguration(
            this WebApplicationBuilder builder)
        {
            var env = builder.Environment;

            var configBuilder = builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);
            
            configBuilder.AddJsonFile("services.json", optional: true);

            if (!env.IsDevelopment()) return (builder, configBuilder);

            var servicesJson = Path.Combine(env.ContentRootPath, "..", "..", "..", "services.json");
            configBuilder
                .AddJsonFile(servicesJson, optional: true)
                .AddEnvironmentVariables();

            return (builder, configBuilder);
        }

        public static IServiceCollection AddCustomMediatR(this IServiceCollection services,
            Type markedType,
            Action<IServiceCollection> doMoreActions = null)
        {
            services.AddMediatR(markedType)
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

            doMoreActions?.Invoke(services);

            return services;
        }

        public static IServiceCollection AddCustomGraphQL(this IServiceCollection services,
            Action<ISchemaConfiguration> schemaConfiguration,
            Action<IServiceCollection> doMoreActions = null)
        {
            services.AddGraphQL(sp => Schema.Create(c =>
                    {
                        c.RegisterServiceProvider(sp);
                        schemaConfiguration.Invoke(c);
                    }),
                    new QueryExecutionOptions
                    {
                        IncludeExceptionDetails = true,
                        TracingPreference = TracingPreference.Always
                    })
                .AddErrorFilter<ValidationErrorFilter>();

            doMoreActions?.Invoke(services);

            return services;
        }

        public static IServiceCollection AddCustomValidators(this IServiceCollection services,
            params Assembly[] validatorAssemblies)
        {
            return services.Scan(scan => scan
                .FromAssemblies(validatorAssemblies)
                .AddClasses(c => c.AssignableTo(typeof(FluentValidation.IValidator<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }

        public static IServiceCollection AddCustomDbContext<TDbContext>(this IServiceCollection services, Assembly anchorAssembly, IConfiguration config)
            where TDbContext : DbContext, IDbFacadeResolver, IDomainEventContext
        {
            services
                .AddDbContext<TDbContext>(options =>
                {
                    options.UseSqlServer(config.GetConnectionString("MainDb"), sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(anchorAssembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 3);
                    });
                });

            services.AddScoped<IDbFacadeResolver>(provider => provider.GetService<TDbContext>());
            services.AddScoped<IDomainEventContext>(provider => provider.GetService<TDbContext>());
            services.AddHostedService<DbContextMigratorHostedService>();

            return services;
        }

        public static IServiceCollection AddCustomGrpc(this IServiceCollection services,
            Action<IServiceCollection> doMoreActions = null)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            services.AddGrpc(options =>
            {
                options.Interceptors.Add<RequestLoggerInterceptor>();
                options.Interceptors.Add<ExceptionHandleInterceptor>();
                options.EnableDetailedErrors = true;
            });

            doMoreActions?.Invoke(services);

            return services;
        }

        public static IServiceCollection AddCustomGrpcClient(this IServiceCollection services,
            Action<IServiceCollection> doMoreActions = null)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            services.AddSingleton<ClientLoggerInterceptor>();

            doMoreActions?.Invoke(services);

            return services;
        }

        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);
            return model;
        }

        [DebuggerStepThrough]
        public static T ConvertTo<T>(this object input)
        {
            return ConvertTo<T>(input.ToString());
        }

        [DebuggerStepThrough]
        public static T ConvertTo<T>(this string input)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                return (T) converter.ConvertFromString(input);
            }
            catch (NotSupportedException)
            {
                return default;
            }
        }

        public static int GetPortOfUrl(this string url)
        {
            return url.Split(":").Last().ConvertTo<int>();
        }

        public static TData ReadData<TData>(this string fileName, string rootFolder)
        {
            var seedData = Path.GetFullPath(fileName, rootFolder);
            Console.WriteLine(seedData);
            using var sr = new StreamReader(seedData);
            var readData = sr.ReadToEnd();
            var models = JsonSerializer.Deserialize<TData>(
                readData,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
            return models;
        }
    }
}