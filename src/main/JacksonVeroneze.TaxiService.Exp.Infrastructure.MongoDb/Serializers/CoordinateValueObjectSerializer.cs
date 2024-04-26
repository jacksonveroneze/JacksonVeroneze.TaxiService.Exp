using JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Serializers;

public class CoordinateValueObjectSerializer : SerializerBase<CoordinateValueObject>
{
    public override CoordinateValueObject Deserialize(
        BsonDeserializationContext context,
        BsonDeserializationArgs args)
    {
        IBsonSerializer<BsonDocument>? serializer =
            BsonSerializer.LookupSerializer<BsonDocument>();

        BsonDocument? doc = serializer.Deserialize(context);

        return new CoordinateValueObject(
            doc.GetValue("Latitude").AsDouble,
            doc.GetValue("Longitude").AsDouble);
    }

    public override void Serialize(
        BsonSerializationContext context,
        BsonSerializationArgs args,
        CoordinateValueObject value)
    {
        BsonDocument doc = new()
        {
            { "Latitude", value.Latitude },
            { "Longitude", value.Longitude }
        };

        BsonDocumentSerializer.Instance.Serialize(context, doc);
    }
}