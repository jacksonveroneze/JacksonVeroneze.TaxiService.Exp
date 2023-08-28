using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Mappings;

public class UserMapping
{
    public static void MapEntities()
    {
        BsonSerializer.RegisterSerializer(new EnumSerializer<UserStatus>(BsonType.String));

        BsonClassMap.RegisterClassMap<UserEntity>(cm =>
        {
            cm.MapMember(conf => conf.Name)
                //.SetSerializer(new StringSerializer())
                .SetIsRequired(true)
                .SetOrder(1);

            cm.MapMember(conf => conf.Birthday)
                .SetSerializer(new DateTimeSerializer())
                .SetIsRequired(true)
                .SetOrder(1);

            cm.MapMember(conf => conf.Status)
                .SetSerializer(new EnumSerializer<UserStatus>(BsonType.String))
                .SetIsRequired(true)
                .SetOrder(2);

            cm.SetIgnoreExtraElements(true);
        });
    }
}
