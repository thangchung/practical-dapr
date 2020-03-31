using System;
using HotChocolate;
using HotChocolate.Configuration;
using HotChocolate.Execution.Configuration;
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
    }
}
