using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using JacksonVeroneze.TemplateWebApi.Application.v1.Services.User;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Extensions;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Application.Services;

public class DeleteUserServiceTests
{
    private readonly Mock<ILogger<DeleteUserService>> _mockLogger;
    private readonly Mock<IUserReadRepository> _mockReadRepository;
    private readonly Mock<IUserWriteRepository> _mockWriteRepository;
    private readonly Mock<IIntegrationEventPublisher> _mockIntegrationEventPublisher;

    private readonly DeleteUserService _service;

    public DeleteUserServiceTests()
    {
        _mockLogger = new Mock<ILogger<DeleteUserService>>();
        _mockReadRepository = new Mock<IUserReadRepository>();
        _mockWriteRepository = new Mock<IUserWriteRepository>();
        _mockIntegrationEventPublisher = new Mock<IIntegrationEventPublisher>();

        _mockLogger.MockLogLevel();

        _service = new DeleteUserService(
            _mockLogger.Object,
            _mockReadRepository.Object,
            _mockWriteRepository.Object,
            _mockIntegrationEventPublisher.Object
        );
    }

    #region success

    [Fact(DisplayName = nameof(DeleteUserService)
                        + nameof(DeleteUserService.DeleteAsync)
                        + "Should Success")]
    public async Task Delete_ReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        Guid userId = Guid.NewGuid();

        UserEntity userEntity = UserEntityBuilder.BuildSingle();

        _mockReadRepository.Setup(mock =>
                mock.GetByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .Callback((Guid id, CancellationToken _) =>
            {
                id.Should()
                    .Be(userId);
            })
            .ReturnsAsync(userEntity);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult<VoidResponse> result = await _service
            .DeleteAsync(userId, CancellationToken.None);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeTrue();

        result.Errors.Should()
            .BeNullOrEmpty();

        _mockReadRepository.Verify(mock =>
            mock.GetByIdAsync(It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()), Times.Once);

        _mockWriteRepository.Verify(mock =>
            mock.DeleteAsync(It.IsAny<UserEntity>(),
                It.IsAny<CancellationToken>()), Times.Once);

        _mockLogger.Verify(nameof(DeleteUserService),
            expectedLogLevel: LogLevel.Information,
            times: Times.Once);
    }

    #endregion

    #region error

    [Fact(DisplayName = nameof(DeleteUserService)
                        + nameof(DeleteUserService.DeleteAsync)
                        + "User Not Exists - Should Error")]
    public async Task Delete_UserNotExists_ShouldReturnError()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        Guid userId = Guid.NewGuid();

        UserEntity? userEntity = null;

        _mockReadRepository.Setup(mock =>
                mock.GetByIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .Callback((Guid id, CancellationToken _) =>
            {
                id.Should()
                    .Be(userId);
            })
            .ReturnsAsync(userEntity);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult<VoidResponse> result = await _service
            .DeleteAsync(userId, CancellationToken.None);

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

        _mockWriteRepository.Verify(mock =>
            mock.DeleteAsync(It.IsAny<UserEntity>(),
                It.IsAny<CancellationToken>()), Times.Never);

        _mockLogger.Verify(nameof(DeleteUserService),
            expectedLogLevel: LogLevel.Warning,
            times: Times.Once);
    }

    #endregion
}
