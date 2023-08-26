using JacksonVeroneze.NET.MongoDB.Extensions;
using JacksonVeroneze.NET.MongoDB.Interfaces;
using JacksonVeroneze.NET.MongoDB.Repository;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Mappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class DatabaseExtension
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration,
        AppConfiguration appConfiguration)
    {
        #region MongoDB

        // CommonMapping.MapEntities();
        // UserMapping.MapEntities();
        //
        // services.AddMongDb(options =>
        // {
        //     options.ConnectionString = appConfiguration.Database!.ConnectionString!;
        //     options.DatabaseName = appConfiguration.Database!.DatabaseName!;
        // });
        //
        // services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));

        #endregion

        #region Dapper

        // services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
        //
        // services.AddScoped<IDbConnection, NpgsqlConnection>(_ =>
        //     new NpgsqlConnection(appConfiguration.Database!.ConnectionString!));
        //
        // SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);
        //
        // AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        #endregion

        #region DynamoDb

        // AWSOptions? config = configuration.GetAWSOptions();
        //
        // services.TryAddAWSService<IAmazonDynamoDB>(config);
        // services.TryAddSingleton<IDynamoDBContext, DynamoDBContext>();

        #endregion

        return services;
    }
}
