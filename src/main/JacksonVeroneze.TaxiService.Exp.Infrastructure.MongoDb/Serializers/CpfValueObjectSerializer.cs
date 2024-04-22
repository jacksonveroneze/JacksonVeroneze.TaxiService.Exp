using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Serializers;

public class CpfValueObjectSerializer(IBsonSerializer<string> serializer)
    : SerializerBase<CpfValueObject>
{
    public override CpfValueObject Deserialize(
        BsonDeserializationContext context,
        BsonDeserializationArgs args)
    {
        return new CpfValueObject(
            serializer.Deserialize(context, args));
    }

    public override void Serialize(
        BsonSerializationContext context,
        BsonSerializationArgs args,
        CpfValueObject value)
    {
        serializer.Serialize(context, args, value.Value!);
    }
}