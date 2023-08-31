using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;
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
        EmailValueObject valueObject =
            EmailValueObjectBuilder.BuildSingle();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        EmailEntity entity = new(valueObject);

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
                        + "throw Exception")]
    public void Create_ThrowException()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        EmailValueObject? valueObject = null;

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        Func<EmailEntity> action =
            () => new EmailEntity(valueObject!);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        action.Should()
            .ThrowExactly<ArgumentNullException>();
    }
}
