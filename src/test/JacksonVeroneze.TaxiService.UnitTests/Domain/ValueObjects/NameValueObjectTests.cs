using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Domain.ValueObjects;

public class NameValueObjectTests
{
    [Theory(DisplayName = nameof(NameValueObject)
                          + nameof(NameValueObject.Create)
                          + "Should Return Success"),
     MemberData(nameof(MockDataValid))]
    public void Create_ShouldReturnSuccess(string value)
    {
        // -------------------------------------------------------
        // Arrange && Act
        // -------------------------------------------------------
        Result<NameValueObject> result =
            NameValueObject.Create(value);

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

        result.Value!.Value.Should()
            .NotBeNullOrEmpty()
            .And.Be(value);
    }

    [Theory(DisplayName = nameof(NameValueObject)
                          + nameof(NameValueObject.Create)
                          + "Should Return Error"),
     MemberData(nameof(MockDataInvalid))]
    public void Create_ShouldReturnError(string value)
    {
        // -------------------------------------------------------
        // Arrange && Act
        // -------------------------------------------------------
        Result<NameValueObject> result =
            NameValueObject.Create(value);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeFalse();

        result.Error.Should()
            .NotBeNull()
            .And.BeEquivalentTo(DomainErrors.User.InvalidName);

        result.Value.Should()
            .BeNull();
    }

    public static readonly object[][] MockDataValid =
    {
        new object[] { "User" },
        new object[] { "User Name" },
    };

    public static readonly object?[][] MockDataInvalid =
    {
        new object?[] { null },
        new object[] { string.Empty },
        new object[] { "" },
        new object[] { "@" },
        new object[] { "Us" }
    };
}
