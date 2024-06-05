using JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.UnitOfWork;
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
        services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();

        services.AddDbContextPool<ApplicationDbContext>((_, options) =>
            options.UseNpgsql(appConfiguration.Database!.WriteConnectionString)
                .ConfigureOptionsDatabase(appConfiguration));

        return services;
    }

    private static void ConfigureOptionsDatabase(
        this DbContextOptionsBuilder optionsBuilder,
        AppConfiguration appConfiguration)
    {
        optionsBuilder
            .EnableDetailedErrors(false)
            .EnableThreadSafetyChecks(false)
            .EnableSensitiveDataLogging(appConfiguration.IsDevelopment)
            .UseSnakeCaseNamingConvention();
    }
}