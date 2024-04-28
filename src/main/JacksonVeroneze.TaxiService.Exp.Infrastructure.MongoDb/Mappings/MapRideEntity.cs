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
                .SetIsRequired(true);

            cm.MapMember(conf => conf.UserId)
                .SetIsRequired(true);

            cm.MapMember(conf => conf.DriverId)
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