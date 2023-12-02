using JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Contexts;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class DatabaseExtension
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

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
            .EnableDetailedErrors(appConfiguration.IsDevelopment)
            .EnableSensitiveDataLogging(appConfiguration.IsDevelopment)
            .UseSnakeCaseNamingConvention();
    }
}
