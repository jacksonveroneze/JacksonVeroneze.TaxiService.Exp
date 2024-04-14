using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Serializers;

public class EmailValueObjectSerializer(IBsonSerializer<string> serializer)
    : SerializerBase<EmailValueObject>
{
    public override EmailValueObject Deserialize(BsonDeserializationContext context,
        BsonDeserializationArgs args)
        => new(serializer.Deserialize(context, args));

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args,
        EmailValueObject value)
        => serializer.Serialize(context, args, value.Value!);
}