using JacksonVeroneze.NET.MongoDB.Interfaces;
using JacksonVeroneze.NET.MongoDB.Repository;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Position;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Repositories.Email;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Repositories.Position;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Repositories.User;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Extensions;

[ExcludeFromCodeCoverage]
public static class AppServicesExtensions
{
    public static IServiceCollection AddMongoDbServices(
        this IServiceCollection services)
    {
        services.AddScoped(typeof(IMongoDbRepository<>), typeof(MongoDbRepository<>));

        #region User

        // Repositories
        services.AddScoped<IUserReadRepository, UserReadRepository>();
        services.AddScoped<IUserWriteRepository, UserWriteRepository>();

        #endregion

        #region Email

        // Repositories
        services.AddScoped<IEmailReadRepository, EmailReadRepository>();
        services.AddScoped<IEmailWriteRepository, EmailWriteRepository>();

        #endregion

        #region Ride

        // Repositories
        services.AddScoped<IRideReadRepository, RideReadRepository>();
        services.AddScoped<IRideWriteRepository, RideWriteRepository>();

        #endregion

        #region Position

        // Repositories
        services.AddScoped<IPositionReadRepository, PositionReadRepository>();
        services.AddScoped<IPositionWriteRepository, PositionWriteRepository>();

        #endregion

        return services;
    }
}