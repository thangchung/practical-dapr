using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N8T.Domain;

namespace N8T.Infrastructure.Data
{
    public static class Extensions
    {
        public static IServiceCollection AddCustomDbContext<TDbContext>(this IServiceCollection services, Type marker, string connString)
            where TDbContext : DbContext, IDbFacadeResolver, IDomainEventContext
        {
            services
                .AddDbContext<TDbContext>(options =>
                {
                    options.UseSqlServer(connString, sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(marker.Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, TimeSpan.FromSeconds(30), null);
                    });
                });

            services.AddScoped<IDbFacadeResolver>(provider => provider.GetService<TDbContext>());
            services.AddScoped<IDomainEventContext>(provider => provider.GetService<TDbContext>());
            services.AddHostedService<DbContextMigratorHostedService>();

            return services;
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
    }
}
