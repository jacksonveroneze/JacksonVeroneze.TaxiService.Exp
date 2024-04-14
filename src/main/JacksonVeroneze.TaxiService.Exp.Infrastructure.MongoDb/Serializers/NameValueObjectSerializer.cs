using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Serializers;

public class NameValueObjectSerializer(IBsonSerializer<string> serializer)
    : SerializerBase<NameValueObject>
{
    public override NameValueObject Deserialize(BsonDeserializationContext context,
        BsonDeserializationArgs args)
        => new(serializer.Deserialize(context, args));

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args,
        NameValueObject value)
        => serializer.Serialize(context, args, value.Value!);
}