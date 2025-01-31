using JacksonVeroneze.NET.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Configurations;

namespace TaxiService.Common.BaseInfrastructure;

[ExcludeFromCodeCoverage]
public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase<TContext>(
        this IServiceCollection services,
        AppConfiguration appConfiguration,
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
                .ConfigureOptionsDatabase(appConfiguration));

        return services;
    }

    private static void ConfigureOptionsDatabase(
        this DbContextOptionsBuilder optionsBuilder,
        AppConfiguration appConfiguration)
    {
        optionsBuilder
            .EnableDetailedErrors(false)
            .EnableSensitiveDataLogging(appConfiguration.IsDevelopment)
            .EnableThreadSafetyChecks()
            .UseSnakeCaseNamingConvention();
    }
}