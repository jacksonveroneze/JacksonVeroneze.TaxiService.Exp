using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Mappings;

public static class MapEmailEntity
{
    public static void Map()
    {
        BsonClassMap.RegisterClassMap<EmailEntity>(cm =>
        {
            cm.MapIdMember(conf => conf.Id)
                .SetIsRequired(true);

            cm.MapMember(conf => conf.Email)
                .SetIsRequired(true);

            cm.MapMember(conf => conf.UserId)
                .SetIsRequired(true);

            cm.SetIgnoreExtraElements(true);
        });
    }
}