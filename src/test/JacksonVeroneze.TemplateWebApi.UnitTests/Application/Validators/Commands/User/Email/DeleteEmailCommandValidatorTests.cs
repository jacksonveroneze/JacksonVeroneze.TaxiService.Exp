using FluentValidation.Results;
using JacksonVeroneze.TemplateWebApi.Application.Commands.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.Validators.Commands.User.Email;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Application.Validators.Commands.User.Email;

public class DeleteEmailCommandValidatorTests
{
    private readonly DeleteEmailCommandValidator _validator = new();

    [Theory(DisplayName = nameof(DeleteEmailCommandValidator)
                          + nameof(DeleteEmailCommandValidator.Validate)
                          + "Should Return Success/Error"),
     MemberData(nameof(MockData))]
    public void Validate_ShouldReturnSuccessOrError(
        Guid id, Guid emailId, bool expected)
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        DeleteEmailCommand command = new(id, emailId);

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
        new object[] { Guid.Empty, Guid.Empty, false },
        new object[] { Guid.Empty, Guid.NewGuid(), false },
        new object[] { Guid.NewGuid(), Guid.Empty, false },
        new object[] { Guid.NewGuid(), Guid.NewGuid(), true },
    };
}
