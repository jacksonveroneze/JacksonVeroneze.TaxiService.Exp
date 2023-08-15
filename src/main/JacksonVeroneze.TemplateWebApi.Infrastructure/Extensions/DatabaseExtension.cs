using JacksonVeroneze.NET.MongoDB.Extensions;
using JacksonVeroneze.NET.MongoDB.Interfaces;
using JacksonVeroneze.NET.MongoDB.Repository;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class DatabaseExtension
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        CommonMapping.MapEntities();
        BankMapping.MapEntities();

        services.AddMongDb(options =>
        {
            options.ConnectionString = appConfiguration.Database!.ConnectionString!;
            options.DatabaseName = appConfiguration.Database!.DatabaseName!;
        });

        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        return services;
    }
}
