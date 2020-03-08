using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using HotChocolate;
using HotChocolate.Configuration;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N8T.Infrastructure.Data;
using N8T.Infrastructure.ValidationModel;
using Path = System.IO.Path;

namespace N8T.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddServiceByInterfaceInAssembly<TRegisteredAssemblyType>(
            this IServiceCollection services, Type interfaceType)
        {
            services.Scan(s =>
                s.FromAssemblyOf<TRegisteredAssemblyType>()
                    .AddClasses(c => c.AssignableTo(interfaceType))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            return services;
        }

        public static IServiceCollection AddCustomMediatR(this IServiceCollection services,
            Type markedType,
            Action<IServiceCollection> doMoreActions = null)
        {
            services.AddMediatR(markedType)
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
            doMoreActions?.Invoke(services);
            return services;
        }

        public static IServiceCollection AddCustomGraphQL(this IServiceCollection services, Action<ISchemaConfiguration> schemaConfiguration, Action<IServiceCollection> doMoreActions = null)
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
                });
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