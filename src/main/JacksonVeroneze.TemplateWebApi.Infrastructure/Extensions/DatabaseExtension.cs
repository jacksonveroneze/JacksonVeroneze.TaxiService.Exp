using System.Data;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Extensions.NETCore.Setup;
using Dapper;
using JacksonVeroneze.NET.MongoDB.Interfaces;
using JacksonVeroneze.NET.MongoDB.Repository;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;

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
        // BankMapping.MapEntities();
        //
        // services.AddMongDb(options =>
        // {
        //     options.ConnectionString = appConfiguration.Database!.ConnectionString!;
        //     options.DatabaseName = appConfiguration.Database!.DatabaseName!;
        // });

        #endregion

        #region Dapper

        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        services.AddScoped<IDbConnection, NpgsqlConnection>(_ =>
            new NpgsqlConnection(appConfiguration.Database!.ConnectionString!));

        SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        #endregion

        #region DynamoDb

        AWSOptions? config = configuration.GetAWSOptions();

        services.TryAddAWSService<IAmazonDynamoDB>(config);
        services.TryAddSingleton<IDynamoDBContext, DynamoDBContext>();

        #endregion

        return services;
    }
}
