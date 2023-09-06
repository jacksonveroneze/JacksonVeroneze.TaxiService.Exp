using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Domain.ValueObjects;

public class CpfValueObjectTests
{
    [Theory(DisplayName = nameof(CpfValueObject)
                          + nameof(CpfValueObject.Create)
                          + "Should Return Success"),
     MemberData(nameof(MockDataValid))]
    public void Create_ShouldReturnSuccess(string value)
    {
        // -------------------------------------------------------
        // Arrange && Act
        // -------------------------------------------------------
        IResult<CpfValueObject> result =
            CpfValueObject.Create(value);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeTrue();

        result.Error.Should()
            .BeNull();

        result.Value.Should()
            .NotBeNull();

        result.Value!.Value.Should()
            .NotBeNull();

        result.Value!.ValueFormatted.Should()
            .NotBeNull();

        result.Value!.Value.Should()
            .NotBeNullOrEmpty();
    }

    [Theory(DisplayName = nameof(CpfValueObject)
                          + nameof(CpfValueObject.Create)
                          + "Should Return Error"),
     MemberData(nameof(MockDataInvalid))]
    public void Create_ShouldReturnError(string value)
    {
        // -------------------------------------------------------
        // Arrange && Act
        // -------------------------------------------------------
        IResult<CpfValueObject> result =
            CpfValueObject.Create(value);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeFalse();

        result.Error.Should()
            .NotBeNull()
            .And.BeEquivalentTo(DomainErrors.User.InvalidCpf);

        result.Value.Should()
            .BeNull();
    }

    public static readonly object[][] MockDataValid =
    {
        new object[] { "06399214939" },
        new object[] { "063.992.149-39" },
    };

    public static readonly object?[][] MockDataInvalid =
    {
        new object?[] { null },
        new object[] { string.Empty },
        new object[] { "" },
        new object[] { "@" },
        new object[] { "063" },
        new object[] { "063.992" },
        new object[] { "063.992-39" }
    };
}
