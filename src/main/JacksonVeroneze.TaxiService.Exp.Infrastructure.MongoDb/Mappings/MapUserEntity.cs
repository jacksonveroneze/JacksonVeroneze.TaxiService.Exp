using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Mappings;

public static class MapUserEntity
{
    public static void Map()
    {
        BsonClassMap.RegisterClassMap<UserEntity>(cm =>
        {
            cm.MapIdMember(conf => conf.Id)
                //.SetSerializer(new GuidSerializer(BsonType.String))
                //.SetIdGenerator(NullIdChecker.Instance)
                .SetIsRequired(true);

            cm.MapMember(conf => conf.Name)
                .SetIsRequired(true);

            cm.MapMember(conf => conf.Gender)
                .SetSerializer(new EnumSerializer<GenderType>())
                .SetIsRequired(true);

            cm.MapMember(conf => conf.Birthday)
                //.SetSerializer(new StringSerializer())
                .SetIsRequired(true);

            cm.MapMember(conf => conf.Cpf)
                .SetIsRequired(true);

            cm.MapMember(conf => conf.Status)
                .SetIsRequired(true);

            cm.SetIgnoreExtraElements(true);
        });
    }
}