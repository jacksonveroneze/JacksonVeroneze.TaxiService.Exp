using JacksonVeroneze.NET.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Configurations;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase<TContext>(
        this IServiceCollection services,
        DatabaseConfiguration databaseConfiguration)
        where TContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(databaseConfiguration);

        services.AddRepository();

        services.AddDbContext<TContext>((_, options) =>
            options.UseNpgsql(databaseConfiguration.ReadConnectionString, conf =>
                    conf.EnableRetryOnFailure()
                        .CommandTimeout(15)
                        .MaxBatchSize(15))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .ConfigureOptionsDatabase());

        return services;
    }

    private static void ConfigureOptionsDatabase(
        this DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .EnableDetailedErrors(false)
            .EnableSensitiveDataLogging(false)
            .EnableThreadSafetyChecks()
            .UseSnakeCaseNamingConvention();
    }
}