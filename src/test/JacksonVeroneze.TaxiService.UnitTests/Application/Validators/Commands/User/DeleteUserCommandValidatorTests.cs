using FluentValidation.Results;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Validators.Commands.User;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Application.Validators.Commands.User;

public class DeleteUserCommandValidatorTests
{
    private readonly DeleteUserCommandValidator _validator = new();

    [Theory(DisplayName = nameof(DeleteUserCommandValidator)
                          + nameof(DeleteUserCommandValidator.Validate)
                          + "Should Return Success/Error"),
     MemberData(nameof(MockData))]
    public void Validate_ShouldReturnSuccessOrError(Guid id, bool expected)
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        DeleteUserCommand command = new() { Id = id };

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
        new object[] { Guid.Empty, false },
        new object[] { Guid.NewGuid(), true },
    };
}
