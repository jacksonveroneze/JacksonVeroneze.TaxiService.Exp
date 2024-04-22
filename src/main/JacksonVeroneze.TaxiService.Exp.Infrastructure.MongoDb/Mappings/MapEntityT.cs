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
            // cm.MapIdMember(conf => conf.Id)
            //     .SetSerializer(new GuidSerializer(BsonType.String))
            //     .SetIdGenerator(NullIdChecker.Instance)
            //     .SetIsRequired(true)
            //     .SetOrder(1);

            cm.MapMember(conf => conf.CreatedAt)
                .SetSerializer(new DateTimeSerializer())
                .SetIsRequired(true)
                .SetOrder(2);

            cm.MapMember(conf => conf.UpdatedAt)
                .SetSerializer(nullableDateTimeSerializer)
                .SetOrder(3);

            cm.MapMember(conf => conf.DeletedAt)
                .SetSerializer(nullableDateTimeSerializer)
                .SetIsRequired(false)
                .SetOrder(4);

            cm.MapMember(conf => conf.Version)
                .SetSerializer(new Int32Serializer())
                .SetIsRequired(true)
                .SetOrder(5);

            cm.SetIgnoreExtraElements(true);
        });
    }
}