using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;

namespace JacksonVeroneze.TaxiService.Exp.Domain.ValueObjects;

public class CoordinateValueObject : ValueObject
{
    public float Latitude { get; }

    public float Longitude { get; }

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

    private static bool IsValid(float latitude, float longitude)
    {
        return latitude is >= -90 and <= 90 &&
               longitude is >= -100 and <= 100;
    }

    public static Result<CoordinateValueObject> Create(
        float latitude, float longitude)
    {
        if (!IsValid(latitude, longitude))
        {
            return Result<CoordinateValueObject>.FromInvalid(
                DomainErrors.Coordinate.InvalidCoordinate);
        }

        CoordinateValueObject valueObject = new(latitude, longitude);

        return Result<CoordinateValueObject>.WithSuccess(valueObject);
    }
}
