using JacksonVeroneze.NET.MongoDB.DomainObjects;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Mappings;

public class BankMapping
{
    public static void MapEntities()
    {
        BsonSerializer.RegisterSerializer(new EnumSerializer<BankStatus>(BsonType.String));

        NullableSerializer<DateTime> nullableDateTimeSerializer =
            new NullableSerializer<DateTime>()
                .WithSerializer(new DateTimeSerializer());

        BsonClassMap.RegisterClassMap<BaseEntity<Guid>>(cm =>
        {
            cm.MapIdMember(conf => conf.Id)
                .SetSerializer(new GuidSerializer(BsonType.String))
                .SetIdGenerator(NullIdChecker.Instance)
                .SetIsRequired(true)
                .SetOrder(1);

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

            cm.MapMember(conf => conf.TenantId)
                .SetSerializer(new GuidSerializer(BsonType.String))
                .SetOrder(6);
        });

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
