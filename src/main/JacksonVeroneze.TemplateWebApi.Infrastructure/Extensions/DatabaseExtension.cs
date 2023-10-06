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
        #region EntityFramework

        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

        services.AddDbContextFactory<ApplicationDbContext>((_, options) =>
            options.UseNpgsql(appConfiguration.Database!.ConnectionString)
                .UseLazyLoadingProxies()
                .EnableDetailedErrors(appConfiguration.IsDevelopment)
                .EnableSensitiveDataLogging(appConfiguration.IsDevelopment)
                .UseSnakeCaseNamingConvention(), ServiceLifetime.Scoped);

        #endregion

        return services;
    }
}
