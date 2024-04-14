using JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ApplicationDbContext = JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Contexts.ApplicationDbContext;
using ReadApplicationDbContext = JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Contexts.ReadApplicationDbContext;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Extensions;

[ExcludeFromCodeCoverage]
public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabaseEfCore(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();

        services.AddDbContext<ReadApplicationDbContext>((_, options) =>
            options.UseNpgsql(appConfiguration.Database!.ReadConnectionString)
                .ConfigureOptionsDatabase(appConfiguration));

        services.AddDbContext<ApplicationDbContext>((_, options) =>
            options.UseNpgsql(appConfiguration.Database!.WriteConnectionString)
                .ConfigureOptionsDatabase(appConfiguration));

        return services;
    }

    private static void ConfigureOptionsDatabase(
        this DbContextOptionsBuilder optionsBuilder,
        AppConfiguration appConfiguration)
    {
        optionsBuilder.UseLazyLoadingProxies()
            .EnableDetailedErrors(false)
            .EnableSensitiveDataLogging(appConfiguration.IsDevelopment)
            .UseSnakeCaseNamingConvention();
    }
}