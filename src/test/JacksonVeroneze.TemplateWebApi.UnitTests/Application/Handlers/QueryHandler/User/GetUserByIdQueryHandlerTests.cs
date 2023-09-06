using AutoMapper;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.TemplateWebApi.Application.Handlers.QueryHandler.User;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.Mappers;
using JacksonVeroneze.TemplateWebApi.Application.Models.User;
using JacksonVeroneze.TemplateWebApi.Application.Queries.User;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Queries.User;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Extensions;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Application.Handlers.QueryHandler.User;

public class GetUserByIdQueryHandlerTests
{
    private readonly GetUserByIdQueryHandler _handler;
    private readonly Mock<ILogger<GetUserByIdQueryHandler>> _mockLogger;
    private readonly Mock<IUserReadRepository> _mockReadRepository;

    public GetUserByIdQueryHandlerTests()
    {
        _mockLogger = new Mock<ILogger<GetUserByIdQueryHandler>>();
        _mockReadRepository = new Mock<IUserReadRepository>();

        _mockLogger.MockLogLevel();

        IMapper mapper = AutoMapperBuilder.Build<UserMapper>();

        _handler = new GetUserByIdQueryHandler(
            _mockLogger.Object,
            mapper,
            _mockReadRepository.Object
        );
    }

    #region success

    [Fact(DisplayName = nameof(GetUserByIdQueryHandler)
                        + nameof(GetUserByIdQueryHandler.Handle)
                        + "Should Success")]
    public async Task Handle_ReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        GetUserByIdQuery query = GetUserByIdQueryBuilder
            .BuildSingle();

        UserEntity userEntity = UserEntityBuilder.BuildSingle();

        _mockReadRepository.Setup(mock =>
                mock.GetByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .Callback((Guid id, CancellationToken _) =>
            {
                id.Should()
                    .Be(query.Id);
            })
            .ReturnsAsync(userEntity);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult<GetUserByIdQueryResponse> result = await _handler
            .Handle(query, CancellationToken.None);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeTrue();

        result.Value.Should()
            .NotBeNull();

        UserResponse? userResponse =
            result.Value!.Data;

        userResponse.Should()
            .NotBeNull();

        userResponse!.Id.Should()
            .NotBeNull();

        userResponse.Name.Should()
            .Be(userEntity.Name);

        userResponse.Birthday.Should()
            .Be(userEntity.Birthday);

        userResponse.Gender.Should()
            .Be(userEntity.Gender);

        userResponse.Status.Should()
            .Be(UserStatus.PendingActivation);

        result.Errors.Should()
            .BeNullOrEmpty();

        _mockReadRepository.Verify(mock =>
            mock.GetByIdAsync(It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()), Times.Once);

        _mockLogger.Verify(nameof(GetUserByIdQueryHandler),
            expectedLogLevel: LogLevel.Information,
            times: Times.Once);
    }

    #endregion

    #region error

    [Fact(DisplayName = nameof(GetUserByIdQueryHandler)
                        + nameof(GetUserByIdQueryHandler.Handle)
                        + "User Not Exists - Should Error")]
    public async Task Handle_UserNotExists_ShouldReturnError()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        GetUserByIdQuery command = GetUserByIdQueryBuilder
            .BuildSingle();

        UserEntity? userEntity = null;

        _mockReadRepository.Setup(mock =>
                mock.GetByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .Callback((Guid id, CancellationToken _) =>
            {
                id.Should()
                    .Be(command.Id);
            })
            .ReturnsAsync(userEntity);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult<GetUserByIdQueryResponse> result = await _handler
            .Handle(command, CancellationToken.None);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeFalse();

        result.Error.Should()
            .NotBeNull()
            .And.BeEquivalentTo(DomainErrors.User.NotFound);

        _mockReadRepository.Verify(mock =>
            mock.GetByIdAsync(It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()), Times.Once);

        _mockLogger.Verify(nameof(GetUserByIdQueryHandler),
            expectedLogLevel: LogLevel.Warning,
            times: Times.Once);
    }

    #endregion
}
