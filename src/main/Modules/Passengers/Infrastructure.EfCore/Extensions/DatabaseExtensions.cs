using JacksonVeroneze.NET.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Passengers.Application.Interfaces.UnitOfWork;
using TaxiService.Modules.Passengers.Infrastructure.Configurations;
using ApplicationDbContext =
    TaxiService.Modules.Passengers.Infrastructure.EfCore.Contexts.ApplicationDbContext;

namespace TaxiService.Modules.Passengers.Infrastructure.EfCore.Extensions;

[ExcludeFromCodeCoverage]
public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabaseEfCore(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        services.AddUnitOfWork()
            .AddRepository();

        services.AddDbContextPool<DbContext, ApplicationDbContext>((_, options) =>
            options.UseNpgsql(appConfiguration.Database!.ReadConnectionString, conf =>
                    conf.EnableRetryOnFailure()
                        .CommandTimeout(15)
                        .MaxBatchSize(15))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .ConfigureOptionsDatabase(appConfiguration));

        services.AddTransient<IUnitOfWork, UnitOfWork>();

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