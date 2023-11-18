using JacksonVeroneze.NET.DomainObjects.ValueObjects;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;

namespace JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

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

    public static IResult<CoordinateValueObject> Create(
        float latitude, float longitude)
    {
        if (latitude == 0 || longitude == 0)
        {
            return Result<CoordinateValueObject>.Invalid(
                DomainErrors.Coordinate.InvalidCoordinate);
        }

        CoordinateValueObject valueObject = new(latitude, longitude);

        return Result<CoordinateValueObject>.Success(valueObject);
    }
}
