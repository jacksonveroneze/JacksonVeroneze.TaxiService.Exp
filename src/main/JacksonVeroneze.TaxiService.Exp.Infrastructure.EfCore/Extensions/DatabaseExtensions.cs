using JacksonVeroneze.NET.EntityFramework.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.UnitOfWork;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ApplicationDbContext =
    JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Contexts.ApplicationDbContext;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Extensions;

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