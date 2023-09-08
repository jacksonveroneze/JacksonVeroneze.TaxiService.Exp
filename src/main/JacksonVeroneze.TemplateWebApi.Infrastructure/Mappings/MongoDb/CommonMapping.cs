// using JacksonVeroneze.NET.DomainObjects.Domain;
// using JacksonVeroneze.TemplateWebApi.Domain.Entities.Base;
// using MongoDB.Bson;
// using MongoDB.Bson.Serialization;
// using MongoDB.Bson.Serialization.IdGenerators;
// using MongoDB.Bson.Serialization.Serializers;
//
// namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Mappings;
//
// public class CommonMapping
// {
//     public static void MapEntities()
//     {
//         string domainNamespace = typeof(Domain.AssemblyReference).Namespace!;
//
//         ObjectSerializer objectSerializer = new(type =>
//             ObjectSerializer.DefaultAllowedTypes(type) ||
//             type.FullName!.StartsWith(domainNamespace, StringComparison.OrdinalIgnoreCase));
//
//         BsonSerializer.RegisterSerializer(objectSerializer);
//
//         NullableSerializer<DateTime> nullableDateTimeSerializer =
//             new NullableSerializer<DateTime>()
//                 .WithSerializer(new DateTimeSerializer());
//
//         BsonClassMap.RegisterClassMap<BaseEntity>(cm =>
//         {
//             cm.SetIgnoreExtraElements(true);
//         });
//
//         BsonClassMap.RegisterClassMap<Entity<Guid>>(cm =>
//         {
//             cm.MapIdMember(conf => conf.Id)
//                 .SetSerializer(new GuidSerializer(BsonType.String))
//                 .SetIdGenerator(new GuidGenerator())
//                 .SetIsRequired(true)
//                 .SetOrder(1);
//
//             cm.MapMember(conf => conf.CreatedAt)
//                 .SetSerializer(new DateTimeSerializer())
//                 .SetIsRequired(true)
//                 .SetOrder(2);
//
//             cm.MapMember(conf => conf.UpdatedAt)
//                 .SetSerializer(nullableDateTimeSerializer)
//                 .SetOrder(3);
//
//             cm.MapMember(conf => conf.DeletedAt)
//                 .SetSerializer(nullableDateTimeSerializer)
//                 .SetIsRequired(false)
//                 .SetOrder(4);
//
//             cm.MapMember(conf => conf.Version)
//                 .SetSerializer(new Int32Serializer())
//                 .SetIsRequired(true)
//                 .SetOrder(5);
//
//             cm.SetIgnoreExtraElements(true);
//         });
//
//         BsonClassMap.RegisterClassMap<Entity>(cm =>
//         {
//             cm.SetIgnoreExtraElements(true);
//         });
//     }
// }
//
//


