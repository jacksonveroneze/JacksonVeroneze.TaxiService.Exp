using System.Data;
using Dapper;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Contexts.EntityFramework;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.UnitOfWork.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        if (appConfiguration.Database!.Type == DatabaseType.Dapper)
        {
            services.AddScoped<IDbConnection, NpgsqlConnection>(_ =>
                new NpgsqlConnection(appConfiguration.Database!.ConnectionString!));

            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.PostgreSQL);

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        #endregion

        #region DynamoDb

        // AWSOptions? config = configuration.GetAWSOptions();
        //
        // services.TryAddAWSService<IAmazonDynamoDB>(config);
        // services.TryAddSingleton<IDynamoDBContext, DynamoDBContext>();

        #endregion

        #region EntityFramework

        if (appConfiguration.Database!.Type == DatabaseType.EntityFramework)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<DbContext, TemplateWebApiContext>((_, options) =>
                options.UseNpgsql(appConfiguration.Database!.ConnectionString)
                    .UseLazyLoadingProxies()
                    .EnableDetailedErrors(appConfiguration.IsDevelopment)
                    .EnableSensitiveDataLogging(appConfiguration.IsDevelopment)
                    .UseSnakeCaseNamingConvention());
        }

        #endregion

        return services;
    }
}
