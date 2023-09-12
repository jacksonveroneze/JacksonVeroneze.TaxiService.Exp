using FluentValidation.Results;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.System;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Validators.Commands.User;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Commands.User;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Application.Validators.Commands.User;

public class CreateUserCommandValidatorTests
{
    private readonly Mock<IDateTime> _mockDatetime;

    private CreateUserCommandValidator _validator;

    public CreateUserCommandValidatorTests()
    {
        _mockDatetime = new Mock<IDateTime>();

        DateOnly now = DateOnly.FromDateTime(DateTime.Now);

        _mockDatetime.SetupGet(mock =>
            mock.DateNow).Returns(now);

        _validator = new CreateUserCommandValidator(
            _mockDatetime.Object);
    }

    [Fact(DisplayName = nameof(CreateUserCommandValidator)
                        + nameof(CreateUserCommandValidator.Validate)
                        + "Should Return Success")]
    public void Validate_Valid_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        CreateUserCommand command = CreateUserCommandBuilder
            .BuildSingle();

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
            .BeTrue();

        result.Errors.Should()
            .BeEmpty();
    }

    [Theory(DisplayName = nameof(CreateUserCommandValidator)
                          + nameof(CreateUserCommandValidator.Validate)
                          + "Should Return Error"
     ), MemberData(nameof(MockData))]
    public void Validate_Valid_ShouldReturnError(
        string name, DateOnly? birthday, Gender? gender,
        string? document, int totalErrors)
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        DateOnly now = DateOnly.FromDateTime(DateTime.Now);

        _mockDatetime.SetupGet(mock =>
            mock.DateNow).Returns(now);

        _validator = new CreateUserCommandValidator(
            _mockDatetime.Object);

        CreateUserCommand command = new()
        {
            Name = name,
            Birthday = birthday,
            Gender = gender,
            Document = document
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
            .BeFalse();

        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.HaveCount(totalErrors);
    }

    public static readonly object?[][] MockData =
    {
        new object?[] { null, null, null, null, 4 },
        new object?[] { "", null, null, null, 4 },
        new object?[] { string.Empty, null, null, null, 4 },
        new object?[] { "a", null, null, null, 4 },
        new object?[] { "ab", null, null, null, 3 },
        new object?[] { "ab", DateOnly.FromDateTime(DateTime.Now.AddDays(1)), null, null, 3 },
        new object?[] { "ab", DateOnly.FromDateTime(DateTime.Now.AddDays(-10)), null, null, 2 },
        new object?[] { "ab", DateOnly.FromDateTime(DateTime.Now.AddDays(-10)), Gender.None, null, 2 },
        new object?[] { "ab", DateOnly.FromDateTime(DateTime.Now.AddDays(-10)), Gender.Male, "", 1 },
        new object?[] { "ab", DateOnly.FromDateTime(DateTime.Now.AddDays(-10)), Gender.Male, "12", 1 },
    };
}

