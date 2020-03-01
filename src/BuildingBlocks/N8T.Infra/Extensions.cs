using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

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

        public static IServiceCollection AddServiceInAssembly(this IServiceCollection services,
            params Assembly[] assemblies)
        {
            if (assemblies == null) return services;

            services.Scan(s => s
                .FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(IDependency)))
                .AsSelf()
                .WithScopedLifetime());

            return services;
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

    public interface IDependency
    {
    }
}