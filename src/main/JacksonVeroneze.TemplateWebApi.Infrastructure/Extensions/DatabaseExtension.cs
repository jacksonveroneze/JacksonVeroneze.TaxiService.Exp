using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Contexts;
using JacksonVeroneze.TemplateWebApi.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

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

        services.AddDbContext<WriteApplicationDbContext>((_, options) =>
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
