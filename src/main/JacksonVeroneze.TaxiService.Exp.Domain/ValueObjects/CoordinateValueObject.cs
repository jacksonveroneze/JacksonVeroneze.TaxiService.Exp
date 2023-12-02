using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;

namespace JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

public class CoordinateValueObject : ValueObject
{
    public float Latitude { get; private set; }

    public float Longitude { get; private set; }

    protected CoordinateValueObject()
    {
    }

    private CoordinateValueObject(
        float latitude, float longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public static implicit operator string(
        CoordinateValueObject value)
        => $"Lat: {value.Latitude} - Lon: {value.Longitude}";

    public static Result<CoordinateValueObject> Create(
        float latitude, float longitude)
    {
        if (latitude == 0 || longitude == 0)
        {
            return Result<CoordinateValueObject>.FromInvalid(
                DomainErrors.Coordinate.InvalidCoordinate);
        }

        CoordinateValueObject valueObject = new(latitude, longitude);

        return Result<CoordinateValueObject>.WithSuccess(valueObject);
    }
}
