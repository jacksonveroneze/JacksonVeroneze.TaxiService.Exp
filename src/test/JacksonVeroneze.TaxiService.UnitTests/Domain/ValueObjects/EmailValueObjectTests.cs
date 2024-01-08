using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Domain.ValueObjects;

public class EmailValueObjectTests
{
    [Theory(DisplayName = nameof(EmailValueObject)
                          + nameof(EmailValueObject.Create)
                          + "Should Return Success"),
     MemberData(nameof(MockDataValid))]
    public void Create_ShouldReturnSuccess(string value)
    {
        // -------------------------------------------------------
        // Arrange && Act
        // -------------------------------------------------------
        Result<EmailValueObject> result =
            EmailValueObject.Create(value);

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

    [Theory(DisplayName = nameof(EmailValueObject)
                          + nameof(EmailValueObject.Create)
                          + "Should Return Error"),
     MemberData(nameof(MockDataInvalid))]
    public void Create_ShouldReturnError(string value)
    {
        // -------------------------------------------------------
        // Arrange && Act
        // -------------------------------------------------------
        Result<EmailValueObject> result =
            EmailValueObject.Create(value);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeFalse();

        result.Error.Should()
            .NotBeNull()
            .And.BeEquivalentTo(DomainErrors.Email.InvalidEmail);

        result.Value.Should()
            .BeNull();
    }

    public static readonly object[][] MockDataValid =
    {
        new object[] { "mail@mail.com" },
    };

    public static readonly object?[][] MockDataInvalid =
    {
        new object?[] { null },
        new object[] { string.Empty },
        new object[] { "" },
        new object[] { "mail" },
        new object[] { "mail@" },
        new object[] { "mail.com" }
    };
}
