using FluentValidation.Results;
using JacksonVeroneze.TemplateWebApi.Application.Queries.User;
using JacksonVeroneze.TemplateWebApi.Application.Validators.Queries.User;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Application.Validators.Queries.User;

public class GetUserByIdQueryValidatorTests
{
    private readonly GetUserByIdQueryValidator _validator = new();

    [Theory(DisplayName = nameof(GetUserByIdQueryValidator)
                          + nameof(GetUserByIdQueryValidator.Validate)
                          + "Should Return Success/Error"),
     MemberData(nameof(MockData))]
    public void Validate_ShouldReturnSuccessOrError(Guid id, bool expected)
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        GetUserByIdQuery query = new(id);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        ValidationResult? result = _validator.Validate(query);

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
