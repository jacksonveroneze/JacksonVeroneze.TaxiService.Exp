using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Domain.Entities;

public class UserEntityTests
{
    #region ctor

    [Fact(DisplayName = nameof(UserEntity)
                        + "Should create with sucess")]
    public void Create_ShouldCreateSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        NameValueObject name = NameValueObjectBuilder.BuildSingle();

        DateOnly birthday = DateOnly.FromDateTime(DateTime.Now);

        GenderType genderType = GenderType.Male;

        CpfValueObject cpf = CpfValueObjectBuilder.BuildSingle();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        UserEntity entity = new(
            name, birthday, genderType, cpf);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        entity.Should()
            .NotBeNull();

        entity.Name.Should()
            .NotBeNull()
            .And.Be(name);

        entity.Birthday.Should()
            .Be(birthday);

        entity.GenderType.Should()
            .Be(genderType);

        entity.Cpf.Should()
            .NotBeNull()
            .And.Be(cpf);

        entity.Status.Should()
            .Be(UserStatus.PendingActivation);

        entity.Events.Should()
            .HaveCount(1);

        entity.Events.Should()
            .OnlyContain(check => check.AggregateId == entity.Id);
    }

    #endregion

    #region active

    [Fact(DisplayName = nameof(UserEntity)
                        + nameof(UserEntity.Activate)
                        + "Should Success")]
    public void Activate_ShouldSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        UserEntity entity = UserEntityBuilder.BuildSingle();

        entity.ClearEvents();

        DateTime now = DateTime.UtcNow;

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult result = entity.Activate(now);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeTrue();

        entity.Status.Should()
            .Be(UserStatus.Active);

        entity.ActivedOnUtc.Should()
            .Be(now);

        // entity.Events.Should()
        //     .HaveCount(1);

        entity.Events.Should()
            .OnlyContain(check => check.AggregateId == entity.Id);
    }

    [Fact(DisplayName = nameof(UserEntity)
                        + nameof(UserEntity.Activate)
                        + "Should Error")]
    public void Activate_ShouldError()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        UserEntity entity = UserEntityBuilder.BuildSingle();

        DateTime now = DateTime.UtcNow;

        entity.Activate(now);

        entity.ClearEvents();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult result = entity.Activate(now);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeFalse();

        result.Error.Should()
            .NotBeNull()
            .And.BeEquivalentTo(DomainErrors.User.AlreadyActivated);

        entity.Events.Should()
            .BeEmpty();
    }

    #endregion

    #region inactivate

    [Fact(DisplayName = nameof(UserEntity)
                        + nameof(UserEntity.Inactivate)
                        + "Should Success")]
    public void Inactivate_ShouldSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        UserEntity entity = UserEntityBuilder.BuildSingle();

        entity.ClearEvents();

        DateTime now = DateTime.UtcNow;

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult result = entity.Inactivate(now);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeTrue();

        entity.Status.Should()
            .Be(UserStatus.Inactive);

        entity.InactivedOnUtc.Should()
            .Be(now);

        entity.Events.Should()
            .HaveCount(1);

        entity.Events.Should()
            .OnlyContain(check => check.AggregateId == entity.Id);
    }

    [Fact(DisplayName = nameof(UserEntity)
                        + nameof(UserEntity.Inactivate)
                        + "Should Error")]
    public void Inactivate_ShouldError()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        UserEntity entity = UserEntityBuilder.BuildSingle();

        DateTime now = DateTime.UtcNow;

        entity.Inactivate(now);

        entity.ClearEvents();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult result = entity.Inactivate(now);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeFalse();

        result.Error.Should()
            .NotBeNull()
            .And.BeEquivalentTo(DomainErrors.User.AlreadyInactivated);

        entity.Events.Should()
            .BeEmpty();
    }

    #endregion

    #region add-email

    [Fact(DisplayName = nameof(UserEntity)
                        + nameof(UserEntity.AddEmail)
                        + "Should Success")]
    public void AddEmail_ShouldSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        UserEntity entity = UserEntityBuilder.BuildSingle();

        EmailEntity emailEntity = EmailEntityBuilder.BuildSingle();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult result = entity.AddEmail(emailEntity);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeTrue();

        entity.Emails.Should()
            .NotBeNullOrEmpty()
            .And.HaveCount(1)
            .And.OnlyContain(item => item == emailEntity);
    }

    [Fact(DisplayName = nameof(UserEntity)
                        + nameof(UserEntity.Inactivate)
                        + "Duplicated - Should Error")]
    public void AddEmail_Duplicated_ShouldError()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        UserEntity entity = UserEntityBuilder.BuildSingle();

        EmailEntity emailEntity = EmailEntityBuilder.BuildSingle();

        entity.AddEmail(emailEntity);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult result = entity.AddEmail(emailEntity);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeFalse();

        result.Error.Should()
            .NotBeNull()
            .And.BeEquivalentTo(DomainErrors.Email.DuplicateEmail);

        entity.Emails.Should()
            .HaveCount(1);
    }

    [Fact(DisplayName = nameof(UserEntity)
                        + nameof(UserEntity.Inactivate)
                        + "NullValue - throw Exception")]
    public void AddEmail_NullValue_ThrowException()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        UserEntity entity = UserEntityBuilder.BuildSingle();

        EmailEntity? emailEntity = null;

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        Func<IResult> action =
            () => entity.AddEmail(emailEntity!);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        action.Should()
            .ThrowExactly<ArgumentNullException>();

        entity.Emails.Should()
            .BeNullOrEmpty();
    }

    #endregion

    #region remove-email

    [Fact(DisplayName = nameof(UserEntity)
                        + nameof(UserEntity.RemoveEmail)
                        + "Should Success")]
    public void RemoveEmail_ShouldSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        UserEntity entity = UserEntityBuilder.BuildSingle();

        EmailEntity emailEntity = EmailEntityBuilder.BuildSingle();

        entity.AddEmail(emailEntity);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult result = entity.RemoveEmail(emailEntity);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeTrue();

        entity.Emails.Should()
            .BeEmpty();
    }

    [Fact(DisplayName = nameof(UserEntity)
                        + nameof(UserEntity.Inactivate)
                        + "NotFound - Should Error")]
    public void RemoveEmail_NotFound_ShouldError()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        UserEntity entity = UserEntityBuilder.BuildSingle();

        EmailEntity emailEntity = EmailEntityBuilder.BuildSingle();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult result = entity.RemoveEmail(emailEntity);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeFalse();

        result.Error.Should()
            .NotBeNull()
            .And.BeEquivalentTo(DomainErrors.Email.NotFound);

        entity.Emails.Should()
            .BeEmpty();
    }

    [Fact(DisplayName = nameof(UserEntity)
                        + nameof(UserEntity.Inactivate)
                        + "NullValue - throw Exception")]
    public void RemoveEmail_NullValue_ThrowException()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        UserEntity entity = UserEntityBuilder.BuildSingle();

        EmailEntity? emailEntity = null;

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        Func<IResult> action =
            () => entity.RemoveEmail(emailEntity!);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        action.Should()
            .ThrowExactly<ArgumentNullException>();

        entity.Emails.Should()
            .BeNullOrEmpty();
    }

    #endregion

    #region get-email-by-id

    [Fact(DisplayName = nameof(UserEntity)
                        + nameof(UserEntity.GetEmailById)
                        + "Should Success")]
    public void GetEmailById_ShouldSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        UserEntity entity = UserEntityBuilder
            .BuildSingle(totalIncludeEmail: 2);

        Guid emailId = entity.Emails.FirstOrDefault()!.Id;

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        EmailEntity? result = entity.GetEmailById(emailId);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result!.Id.Should()
            .Be(emailId);
    }

    [Fact(DisplayName = nameof(UserEntity)
                        + nameof(UserEntity.GetEmailById)
                        + "NotFound - Should Error")]
    public void GetEmailById_NotFound_ShouldError()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        UserEntity entity = UserEntityBuilder
            .BuildSingle(totalIncludeEmail: 2);

        Guid emailId = Guid.NewGuid();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        EmailEntity? result = entity.GetEmailById(emailId);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .BeNull();
    }

    #endregion

    #region exists-email-by-value

    [Fact(DisplayName = nameof(UserEntity)
                        + nameof(UserEntity.ExistsEmailByValue)
                        + "Exists - Should Success")]
    public void ExistsEmailByValue_Exists_ShouldSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        UserEntity entity = UserEntityBuilder
            .BuildSingle(totalIncludeEmail: 5);

        EmailEntity email = entity.Emails.FirstOrDefault()!;

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        bool result = entity.ExistsEmailByValue(email.Email);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .BeTrue();
    }

    [Fact(DisplayName = nameof(UserEntity)
                        + nameof(UserEntity.ExistsEmailByValue)
                        + "NotExists - Should Success")]
    public void ExistsEmailByValue_NotExists_ShouldSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        UserEntity entity = UserEntityBuilder
            .BuildSingle(totalIncludeEmail: 5);

        EmailValueObject valueObject = EmailValueObjectBuilder
            .BuildSingle();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        bool result = entity.ExistsEmailByValue(valueObject);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .BeFalse();
    }

    #endregion
}


// 95% das transações foram atendidas na latencia
