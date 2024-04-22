using JacksonVeroneze.NET.MongoDB.Extensions;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Mappings;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Serializers;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Extensions;

[ExcludeFromCodeCoverage]
public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabaseMongoDb(
        this IServiceCollection services)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

#pragma warning disable CS0618
        BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
        BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
#pragma warning restore CS0618

        IBsonSerializer<string>? stringBsonSerializer = BsonSerializer
            .SerializerRegistry.GetSerializer<string>();

        BsonSerializer.RegisterSerializer(new NameValueObjectSerializer(stringBsonSerializer));
        BsonSerializer.RegisterSerializer(new CpfValueObjectSerializer(stringBsonSerializer));
        BsonSerializer.RegisterSerializer(new EmailValueObjectSerializer(stringBsonSerializer));

        MapEntityT.Map();
        MapUserEntity.Map();
        MapEmailEntity.Map();
        MapRideEntity.Map();
        MapPositionEntity.Map();

        services.AddMongDb(conf =>
        {
            conf.ConnectionString = "mongodb://admin:admin@10.0.0.199:27017?connectTimeoutMS=5000";
            conf.DatabaseName = "Users";
        });

        return services;
    }
}