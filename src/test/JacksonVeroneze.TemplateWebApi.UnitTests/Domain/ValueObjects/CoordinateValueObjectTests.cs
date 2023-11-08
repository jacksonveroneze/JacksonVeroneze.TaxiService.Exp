using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Domain.ValueObjects;

public class CoordinateValueObjectTests
{
    [Theory(DisplayName = nameof(CoordinateValueObject)
                          + nameof(CoordinateValueObject.Create)
                          + "Should Return Success"),
     MemberData(nameof(MockDataValid))]
    public void Create_ShouldReturnSuccess(
        float latitude, float longitude)
    {
        // -------------------------------------------------------
        // Arrange && Act
        // -------------------------------------------------------
        IResult<CoordinateValueObject> result =
            CoordinateValueObject.Create(latitude, longitude);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeTrue();

        result.Error.Should()
            .BeNull();

        result.Value.Should()
            .NotBeNull();

        result.Value!.Latitude.Should()
            .BeApproximately(latitude, 0.1F);

        result.Value!.Longitude.Should()
            .BeApproximately(longitude, 0.1F);
    }

    [Theory(DisplayName = nameof(CoordinateValueObject)
                          + nameof(CoordinateValueObject.Create)
                          + "Should Return Error"),
     MemberData(nameof(MockDataInvalid))]
    public void Create_ShouldReturnError(
        float latitude, float longitude)
    {
        // -------------------------------------------------------
        // Arrange && Act
        // -------------------------------------------------------
        IResult<CoordinateValueObject> result =
            CoordinateValueObject.Create(latitude, longitude);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeFalse();

        result.Error.Should()
            .NotBeNull()
            .And.BeEquivalentTo(DomainErrors.Coordinate.InvalidCoordinate);

        result.Value.Should()
            .BeNull();
    }

    public static readonly object[][] MockDataValid =
    {
        new object[] { 12.12F, 11.12455F },
        new object[] { 27.12F, 28.125455F },
    };

    public static readonly object?[][] MockDataInvalid =
    {
        new object?[] { null, null },
        new object?[] { 12.125454F, null },
        new object?[] { null, 12.1254545F }
    };
}
