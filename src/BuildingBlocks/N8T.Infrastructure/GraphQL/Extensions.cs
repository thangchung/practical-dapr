using System;
using System.Linq;
using System.Reflection;
using HotChocolate;
using HotChocolate.Configuration;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using N8T.Infrastructure.GraphQL.Errors;

namespace N8T.Infrastructure.GraphQL
{
    public static class Extensions
    {
        public static IServiceCollection AddCustomGraphQL(this IServiceCollection services,
            Action<ISchemaConfiguration> schemaConfiguration,
            Action<IServiceCollection> doMoreActions = null)
        {
            services.AddDataLoaderRegistry();

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

        public static ISchemaConfiguration RegisterObjectTypes(this ISchemaConfiguration schemaConfiguration, Assembly graphTypeAssembly)
        {
            var objectTypes = graphTypeAssembly
                .GetTypes()
                .Where(type => typeof(ObjectType).IsAssignableFrom(type));

            foreach (var objectType in objectTypes)
            {
                schemaConfiguration.RegisterType(objectType);
            }

            return schemaConfiguration;
        }
    }
}
