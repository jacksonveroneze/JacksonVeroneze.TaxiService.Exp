using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Mappings;

public static class MapEmailEntity
{
    public static void Map()
    {
        BsonClassMap.RegisterClassMap<EmailEntity>(cm =>
        {
            cm.MapIdMember(conf => conf.Id)
                //.SetSerializer(new GuidSerializer(BsonType.String))
                //.SetIdGenerator(NullIdChecker.Instance)
                .SetIsRequired(true);

            cm.MapMember(conf => conf.Email)
                .SetIsRequired(true);

            cm.MapMember(conf => conf.UserId)
                .SetSerializer(new GuidSerializer())
                .SetIsRequired(true);

            cm.SetIgnoreExtraElements(true);
        });
    }
}