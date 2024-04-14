using JacksonVeroneze.NET.DomainObjects.Domain;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Enums;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Mappings;

public static class MapEntities
{
    public static void Map()
    {
        NullableSerializer<DateTime> nullableDateTimeSerializer =
            new NullableSerializer<DateTime>()
                .WithSerializer(new DateTimeSerializer());

        IBsonSerializer<string>? stringBsonSerializer = BsonSerializer
            .SerializerRegistry.GetSerializer<string>();

        BsonSerializer.RegisterSerializer(new NameValueObjectSerializer(stringBsonSerializer));
        BsonSerializer.RegisterSerializer(new CpfValueObjectSerializer(stringBsonSerializer));
        BsonSerializer.RegisterSerializer(new EmailValueObjectSerializer(stringBsonSerializer));

        BsonClassMap.RegisterClassMap<Entity<Guid>>(cm =>
        {
            // cm.MapIdMember(conf => conf.Id)
            //     .SetSerializer(new GuidSerializer(BsonType.String))
            //     .SetIdGenerator(NullIdChecker.Instance)
            //     .SetIsRequired(true)
            //     .SetOrder(1);

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

            cm.SetIgnoreExtraElements(true);
        });

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