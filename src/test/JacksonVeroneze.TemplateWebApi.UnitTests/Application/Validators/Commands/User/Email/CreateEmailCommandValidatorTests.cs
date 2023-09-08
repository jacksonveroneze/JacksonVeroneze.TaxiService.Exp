using FluentValidation.Results;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.v1.Validators.Commands.User.Email;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Application.Validators.Commands.User.Email;

public class CreateEmailCommandValidatorTests
{
    private readonly CreateEmailCommandValidator _validator = new();

    [Theory(DisplayName = nameof(CreateEmailCommandValidator)
                          + nameof(CreateEmailCommandValidator.Validate)
                          + "Should Return Success/Error"),
     MemberData(nameof(MockData))]
    public void Validate_ShouldReturnSuccessOrError(
        Guid id, string? email, bool expected)
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        CreateEmailCommand command = new()
        {
            Id = id,
            Email = email
        };

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        ValidationResult? result = _validator.Validate(command);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsValid.Should()
            .Be(expected);
    }

    public static readonly object[][] MockData =
    {
        new object[] { Guid.Empty, "", false },
        new object[] { Guid.NewGuid(), "", false },
        new object[] { Guid.NewGuid(), "1234", false },
        new object[] { Guid.NewGuid(), "mail", false },
        new object[] { Guid.NewGuid(), "mail#mail.com", false },
        new object[] { Guid.NewGuid(), "mail@mail.com", true }
    };
}
