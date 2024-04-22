using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Mappings;

public static class MapPositionEntity
{
    public static void Map()
    {
        BsonClassMap.RegisterClassMap<PositionEntity>(cm =>
        {
            cm.MapIdMember(conf => conf.Id)
                //.SetSerializer(new GuidSerializer(BsonType.String))
                //.SetIdGenerator(NullIdChecker.Instance)
                .SetIsRequired(true);

            cm.MapMember(conf => conf.RideId)
                .SetIsRequired(true);

            cm.MapMember(conf => conf.Position)
                //.SetSerializer(new StringSerializer())
                .SetIsRequired(true);

            cm.SetIgnoreExtraElements(true);
        });
    }
}