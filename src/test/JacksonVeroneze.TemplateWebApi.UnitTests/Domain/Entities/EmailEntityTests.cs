using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Domain.Entities;

public class EmailEntityTests
{
    [Fact(DisplayName = nameof(EmailEntity)
                        + "Should Return Success")]
    public void Create_ShouldReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        UserEntity user = UserEntityBuilder.BuildSingle();

        EmailValueObject valueObject =
            EmailValueObjectBuilder.BuildSingle();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        EmailEntity entity = new(user, valueObject);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        entity.Should()
            .NotBeNull();

        entity.Email.Should()
            .NotBeNull()
            .And.Be(valueObject);
    }

    [Fact(DisplayName = nameof(EmailEntity)
                        + "Invalid User - throw Exception")]
    public void Create_Invaliduser_ThrowException()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        UserEntity? user = null;

        EmailValueObject valueObject =
            EmailValueObjectBuilder.BuildSingle();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        Func<EmailEntity> action =
            () => new EmailEntity(user!, valueObject!);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        action.Should()
            .ThrowExactly<ArgumentNullException>();
    }

    [Fact(DisplayName = nameof(EmailEntity)
                        + "Invalid Email - throw Exception")]
    public void Create_InvalidEmail_ThrowException()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        UserEntity user = UserEntityBuilder.BuildSingle();

        EmailValueObject? valueObject = null;

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        Func<EmailEntity> action =
            () => new EmailEntity(user, valueObject!);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        action.Should()
            .ThrowExactly<ArgumentNullException>();
    }
}
