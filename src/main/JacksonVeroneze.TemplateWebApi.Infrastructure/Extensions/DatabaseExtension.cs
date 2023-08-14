using JacksonVeroneze.NET.MongoDB.Extensions;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class DatabaseExtension
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services)
    {
        BankMapping.MapEntities();

        services.AddMongDb(options =>
        {
            options.ConnectionString = "mongodb://admin:admin@10.0.0.199:27017";
            options.DatabaseName = "Pix";
        });

        return services;
    }
}
