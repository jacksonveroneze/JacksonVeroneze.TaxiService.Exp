using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Mappings;

public class BankMapping
{
    public static void MapEntities()
    {
        BsonSerializer.RegisterSerializer(new EnumSerializer<BankStatus>(BsonType.String));

        BsonClassMap.RegisterClassMap<BankEntity>(cm =>
        {
            cm.MapMember(conf => conf.Name)
                .SetSerializer(new StringSerializer())
                .SetIsRequired(true)
                .SetOrder(1);

            cm.MapMember(conf => conf.Status)
                .SetSerializer(new EnumSerializer<BankStatus>(BsonType.String))
                .SetIsRequired(true)
                .SetOrder(2);

            cm.SetIgnoreExtraElements(true);
        });
    }
}
