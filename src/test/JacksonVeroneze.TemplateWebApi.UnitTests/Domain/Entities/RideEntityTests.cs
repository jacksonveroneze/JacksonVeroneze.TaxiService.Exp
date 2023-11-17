using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Domain.Entities;

public class RideEntityTests
{
    #region ctor

    [Fact(DisplayName = nameof(RideEntity)
                        + "Should create with sucess")]
    public void Create_ShouldCreateWithSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        UserEntity user = UserEntityBuilder.BuildSingle();

        CoordinateValueObject coordinateFrom =
            CoordinateValueObjectBuilder.BuildSingle();

        CoordinateValueObject coordinateTo =
            CoordinateValueObjectBuilder.BuildSingle();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        RideEntity entity = new(
            user, coordinateFrom, coordinateTo);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        entity.Should()
            .NotBeNull();

        entity.User.Should()
            .NotBeNull()
            .And.Be(user);

        entity.From.Should()
            .NotBeNull()
            .And.Be(coordinateFrom);

        entity.To.Should()
            .NotBeNull()
            .And.Be(coordinateTo);

        entity.Status.Should()
            .Be(RideStatus.Requested);
    }

    #endregion

    #region Position

    [Fact(DisplayName = nameof(RideEntity)
                        + nameof(RideEntity.AddPosition)
                        + "Should with success")]
    public void AddPosition_ShouldWithSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        RideEntity entity = RideEntityBuilder.BuildSingle();

        PositionEntity position = PositionEntityBuilder
            .BuildSingle(entity);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult result = entity.AddPosition(position);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        entity.Positions.Should()
            .NotBeNull()
            .And.HaveCount(1);
    }

    #endregion

    #region Accept

    [Fact(DisplayName = nameof(RideEntity)
                        + nameof(RideEntity.Accept)
                        + "Should with success")]
    public void Accept_ShouldWithSuccess()
    {
        // // -------------------------------------------------------
        // // Arrange
        // // -------------------------------------------------------
        // RideEntity entity = RideEntityBuilder.BuildSingle();
        //
        // // -------------------------------------------------------
        // // Act
        // // -------------------------------------------------------
        // IResult result = entity.Accept();
        //
        // // -------------------------------------------------------
        // // Assert
        // // -------------------------------------------------------
        // result.Should()
        //     .NotBeNull();
        //
        // result.IsSuccess.Should()
        //     .BeTrue();
        //
        // entity.Status.Should()
        //     .Be(RideStatus.Accepted);
    }

    [Fact(DisplayName = nameof(RideEntity)
                        + nameof(RideEntity.Accept)
                        + "Invalid current status - Return error")]
    public void Accept_InvalidCurrentStatus_ReturnError()
    {
        // // -------------------------------------------------------
        // // Arrange
        // // -------------------------------------------------------
        // RideEntity entity = RideEntityBuilder.BuildSingle();
        // entity.Accept();
        //
        // // -------------------------------------------------------
        // // Act
        // // -------------------------------------------------------
        // IResult result = entity.Accept();
        //
        // // -------------------------------------------------------
        // // Assert
        // // -------------------------------------------------------
        // result.Should()
        //     .NotBeNull();
        //
        // result.IsSuccess.Should()
        //     .BeFalse();
        //
        // entity.Status.Should()
        //     .Be(RideStatus.Accepted);
    }

    #endregion
}
