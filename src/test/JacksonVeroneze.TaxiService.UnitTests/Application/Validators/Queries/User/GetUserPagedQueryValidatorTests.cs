using FluentValidation.Results;
using JacksonVeroneze.TemplateWebApi.Application.v1.Queries.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Validators.Queries.User;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Application.Validators.Queries.User;

public class GetUserPagedQueryValidatorTests
{
    private readonly GetUserPagedQueryValidator _validator = new();

    [Theory(DisplayName = nameof(GetUserPagedQueryValidator)
                          + nameof(GetUserPagedQueryValidator.Validate)
                          + "Should Return Success/Error"),
     MemberData(nameof(MockData))]
    public void Validate_ShouldReturnSuccessOrError(
        string? name, UserStatus? status,
        int page, int pageSize, int totalErrors)
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        GetUserPagedQuery query = new()
        {
            Name = name,
            Status = status,
            Page = page,
            PageSize = pageSize
        };

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
            .Be(totalErrors == 0);

        result.Errors.Should()
            .HaveCount(totalErrors);
    }

    public static readonly object?[][] MockData =
    {
        new object?[] { string.Empty, UserStatus.None, 0, 0, 3 },
        new object?[] { "", UserStatus.None, 0, 0, 3 },
        new object?[] { "a", UserStatus.None, 0, 0, 4 },
        new object?[] { "name", UserStatus.None, 0, 0, 3 },
        new object?[] { "name", UserStatus.Active, 0, 0, 2 },
        new object?[] { "name", UserStatus.Active, 1, 0, 1 },
        new object?[] { null, null, 0, 0, 2 },
        new object?[] { null, null, 1, 0, 1 },
        new object?[] { null, null, 1, 1, 0 },
        new object?[] { "name", UserStatus.Active, 1, 1, 0 },
    };
}
