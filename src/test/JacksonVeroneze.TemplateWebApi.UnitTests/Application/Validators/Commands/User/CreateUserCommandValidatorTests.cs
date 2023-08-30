using FluentValidation.Results;
using JacksonVeroneze.TemplateWebApi.Application.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.Validators.Commands.User;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Commands;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Application.Validators.Commands.User;

public class CreateUserCommandValidatorTests
{
    private readonly CreateUserCommandValidator _validator = new();

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

    [Theory(
         DisplayName = nameof(CreateUserCommandValidator)
                       + nameof(CreateUserCommandValidator.Validate)
                       + "Should Return Error"
     ), MemberData(nameof(CorrectData))]
    public void Validate_Valid_ShouldReturnError(
        string name, DateOnly? birthday, Gender? gender, int totalErrors)
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        CreateUserCommand command = new CreateUserCommand()
        {
            Name = name,
            Birthday = birthday,
            Gender = gender
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

    public static readonly object[][] CorrectData =
    {
        new object[] { null!, (null as DateOnly?)!, (null as Gender?)!, 3 },
        new object[] { StringUtils.Generate(0), (null as DateOnly?)!, (null as Gender?)!, 3 },
    };
}

public static class StringUtils
{
    public static string Generate(int length)
    {
        const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                                "abcdefghijklmnopqrstuvwxyz" +
                                "0123456789";

        char[] result = Enumerable.Repeat(alphabet, length)
            .Select(s => s[new Random().Next(s.Length)]).ToArray();

        return new string(result);
    }
}
