using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Mappings;

public static class MapRideEntity
{
    public static void Map()
    {
        BsonClassMap.RegisterClassMap<RideEntity>(cm =>
        {
            cm.MapIdMember(conf => conf.Id)
                //.SetSerializer(new GuidSerializer(BsonType.String))
                //.SetIdGenerator(NullIdChecker.Instance)
                .SetIsRequired(true);

            cm.MapMember(conf => conf.UserId)
                //.SetSerializer(new GuidSerializer())
                .SetIsRequired(true);

            cm.MapMember(conf => conf.DriverId)
                //.SetSerializer(new GuidSerializer())
                .SetIsRequired(false);

            cm.MapMember(conf => conf.Fare)
                .SetIsRequired(false);

            cm.MapMember(conf => conf.Distance)
                .SetIsRequired(false);

            cm.MapMember(conf => conf.Distance)
                .SetIsRequired(false);

            cm.MapMember(conf => conf.CoordinateFrom)
                .SetIsRequired(true);

            cm.MapMember(conf => conf.CoordinateTo)
                .SetIsRequired(true);

            cm.MapMember(conf => conf.Status)
                .SetIsRequired(true);

            cm.SetIgnoreExtraElements(true);
        });
    }
}