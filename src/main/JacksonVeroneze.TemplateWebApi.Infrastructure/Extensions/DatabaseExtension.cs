using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Contexts.EntityFramework;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.UnitOfWork.EntityFramework;
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

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<DbContext, ApplicationDbContext>((_, options) =>
            options.UseNpgsql(appConfiguration.Database!.ConnectionString)
                .UseLazyLoadingProxies()
                .EnableDetailedErrors(appConfiguration.IsDevelopment)
                .EnableSensitiveDataLogging(appConfiguration.IsDevelopment)
                .UseSnakeCaseNamingConvention());

        #endregion

        return services;
    }
}
