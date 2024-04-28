using JacksonVeroneze.NET.DomainObjects.Domain;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Mappings;

public static class MapEntityT
{
    public static void Map()
    {
        NullableSerializer<DateTime> nullableDateTimeSerializer =
            new NullableSerializer<DateTime>()
                .WithSerializer(new DateTimeSerializer());

        BsonClassMap.RegisterClassMap<Entity<Guid>>(cm =>
        {
            cm.MapMember(conf => conf.CreatedAt)
                .SetSerializer(new DateTimeSerializer())
                .SetIsRequired(true);

            cm.MapMember(conf => conf.UpdatedAt)
                .SetSerializer(nullableDateTimeSerializer);

            cm.MapMember(conf => conf.DeletedAt)
                .SetSerializer(nullableDateTimeSerializer)
                .SetIsRequired(false);

            cm.MapMember(conf => conf.Version)
                .SetIsRequired(true);

            cm.SetIgnoreExtraElements(true);
        });
    }
}