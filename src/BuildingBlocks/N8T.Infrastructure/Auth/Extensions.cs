using System;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace N8T.Infrastructure.Auth
{
    public static class Extensions
    {
        public static IServiceCollection AddCustomAuth(this IServiceCollection services,
            IConfiguration config,
            Action<IServiceCollection> configureMoreActions = null,
            Action<JwtBearerOptions> configureOptions = null)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    config.Bind("JwtAuth", options);
                    configureOptions?.Invoke(options);
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(JwtBearerDefaults.AuthenticationScheme, policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireClaim(ClaimTypes.Name);
                });
            });

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(AuthBehavior<,>));

            configureMoreActions?.Invoke(services);

            return services;
        }
    }
}
