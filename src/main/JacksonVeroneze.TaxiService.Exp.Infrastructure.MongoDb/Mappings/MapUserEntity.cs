using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using MongoDB.Bson.Serialization;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Mappings;

public static class MapUserEntity
{
    public static void Map()
    {
        BsonClassMap.RegisterClassMap<UserEntity>(cm =>
        {
            cm.MapIdMember(conf => conf.Id)
                .SetIsRequired(true);

            cm.MapMember(conf => conf.Name)
                .SetIsRequired(true);

            cm.MapMember(conf => conf.Gender)
                .SetIsRequired(true);

            cm.MapMember(conf => conf.Birthday)
                .SetIsRequired(true);

            cm.MapMember(conf => conf.Cpf)
                .SetIsRequired(true);

            cm.MapMember(conf => conf.Status)
                .SetIsRequired(true);

            cm.SetIgnoreExtraElements(true);
        });
    }
}