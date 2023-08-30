using FluentValidation.Results;
using JacksonVeroneze.TemplateWebApi.Application.Queries.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.Validators.Queries.User.Email;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Application.Validators.Queries.User.Email;

public class GetAllEmailsByUserIdQueryValidatorTests
{
    private readonly GetAllEmailsByUserIdQueryValidator _validator = new();

    [Theory(DisplayName = nameof(GetAllEmailsByUserIdQueryValidator)
                          + nameof(GetAllEmailsByUserIdQueryValidator.Validate)
                          + "Should Return Success/Error"),
     MemberData(nameof(MockData))]
    public void Validate_ShouldReturnSuccessOrError(Guid id, bool expected)
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        GetAllEmailsByUserIdQuery query = new(id);

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
