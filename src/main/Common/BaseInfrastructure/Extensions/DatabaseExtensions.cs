using JacksonVeroneze.NET.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Configurations;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class DatabaseExtensions
{
    private const int DefaultCommandTimeout = 5;

    public static IServiceCollection AddWriteDatabase<TContext>(
        this IServiceCollection services,
        DatabaseConfiguration databaseConfiguration)
        where TContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(databaseConfiguration);

        services.AddRepository();

        services.AddDbContext<TContext>((_, options) =>
            options.UseNpgsql(databaseConfiguration.WriteConnectionString, conf =>
                {
                    conf.UseNetTopologySuite();
                    
                    conf.EnableRetryOnFailure()
                        .CommandTimeout(DefaultCommandTimeout);
                })
                .ConfigureOptionsDatabase());

        return services;
    }

    public static IServiceCollection AddReadDatabase<TContext>(
        this IServiceCollection services,
        DatabaseConfiguration databaseConfiguration)
        where TContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(databaseConfiguration);

        services.AddDbContext<TContext>((_, options) =>
            options.UseNpgsql(databaseConfiguration.ReadConnectionString, conf =>
                {
                    conf.UseNetTopologySuite();
                    
                    conf.EnableRetryOnFailure()
                        .CommandTimeout(DefaultCommandTimeout);
                })
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .ConfigureOptionsDatabase());

        return services;
    }

    private static void ConfigureOptionsDatabase(
        this DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .EnableThreadSafetyChecks()
            .UseSnakeCaseNamingConvention();
    }
}
