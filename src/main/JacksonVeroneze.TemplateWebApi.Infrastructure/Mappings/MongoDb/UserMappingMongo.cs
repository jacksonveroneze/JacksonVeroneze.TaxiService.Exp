// using JacksonVeroneze.TemplateWebApi.Domain.Enums;
// using JacksonVeroneze.TemplateWebApi.Infrastructure.Models;
// using MongoDB.Bson;
// using MongoDB.Bson.Serialization;
// using MongoDB.Bson.Serialization.Serializers;
//
// namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Mappings;
//
// public class UserMapping
// {
//     public static void MapEntities()
//     {
//         BsonSerializer.RegisterSerializer(new EnumSerializer<UserStatus>(BsonType.String));
//
//         BsonClassMap.RegisterClassMap<UserModel>(cm =>
//         {
//             cm.MapMember(conf => conf.Name)
//                 //.SetSerializer(new NameSerializer())
//                 .SetIsRequired(true)
//                 .SetOrder(1);
//
//             cm.MapMember(conf => conf.Birthday)
//                 //.SetSerializer(new Dateon())
//                 .SetIsRequired(true)
//                 .SetOrder(1);
//
//             cm.MapMember(conf => conf.Status)
//                 .SetSerializer(new EnumSerializer<UserStatus>(BsonType.String))
//                 .SetIsRequired(true)
//                 .SetOrder(2);
//
//             cm.SetIgnoreExtraElements(true);
//         });
//     }
// }


