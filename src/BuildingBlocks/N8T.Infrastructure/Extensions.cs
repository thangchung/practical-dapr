using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N8T.Infrastructure.Data;
using N8T.Infrastructure.Logging;
using N8T.Infrastructure.ValidationModel;
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
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            return (builder, configBuilder);
        }

        [DebuggerStepThrough]
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

        [DebuggerStepThrough]
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

        [DebuggerStepThrough]
        public static int GetPortOfUrl(this string url)
        {
            return url.Split(":").Last().ConvertTo<int>();
        }

        [DebuggerStepThrough]
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

        [DebuggerStepThrough]
        public static string SerializeObject(this object obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }
}
