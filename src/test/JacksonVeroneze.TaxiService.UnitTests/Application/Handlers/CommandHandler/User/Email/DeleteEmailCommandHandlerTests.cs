using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Commands.User.Email;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Extensions;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Application.Handlers.CommandHandler.User.Email;

public class DeleteEmailCommandHandlerTests
{
    private readonly DeleteEmailCommandHandler _handler;
    private readonly Mock<ILogger<DeleteEmailCommandHandler>> _mockLogger;
    private readonly Mock<IUserReadRepository> _mockReadRepository;
    private readonly Mock<IUserWriteRepository> _mockWriteRepository;

    public DeleteEmailCommandHandlerTests()
    {
        _mockLogger = new Mock<ILogger<DeleteEmailCommandHandler>>();
        _mockReadRepository = new Mock<IUserReadRepository>();
        _mockWriteRepository = new Mock<IUserWriteRepository>();

        _mockLogger.MockLogLevel();

        _handler = new DeleteEmailCommandHandler(
            _mockLogger.Object,
            _mockReadRepository.Object,
            _mockWriteRepository.Object
        );
    }

    #region success

    [Fact(DisplayName = nameof(DeleteEmailCommandHandler)
                        + nameof(DeleteEmailCommandHandler.Handle)
                        + "Should Success")]
    public async Task Handle_ReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        UserEntity userEntity = UserEntityBuilder
            .BuildSingle(totalIncludeEmail: 2);

        DeleteEmailCommand command = DeleteEmailCommandBuilder
            .BuildSingle(userEntity.Id, userEntity.Emails.First().Id);

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
        Result<VoidResponse> result = await _handler
            .Handle(command, CancellationToken.None);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeTrue();

        result.Errors.Should()
            .BeNullOrEmpty();

        userEntity.Emails.Should()
            .NotBeNullOrEmpty()
            .And.HaveCount(1);

        _mockReadRepository.Verify(mock =>
            mock.GetByIdAsync(It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()), Times.Once);

        _mockWriteRepository.Verify(mock =>
            mock.UpdateAsync(It.IsAny<UserEntity>(),
                It.IsAny<CancellationToken>()), Times.Once);

        _mockLogger.Verify(nameof(DeleteEmailCommandHandler),
            expectedLogLevel: LogLevel.Information,
            times: Times.Once);
    }

    #endregion

    #region error

    [Fact(DisplayName = nameof(DeleteEmailCommandHandler)
                        + nameof(DeleteEmailCommandHandler.Handle)
                        + "User Not Exists - Should Error")]
    public async Task Handle_UserNotExists_ShouldReturnError()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        DeleteEmailCommand command = DeleteEmailCommandBuilder
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
        Result<VoidResponse> result = await _handler
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

        _mockWriteRepository.Verify(mock =>
            mock.UpdateAsync(It.IsAny<UserEntity>(),
                It.IsAny<CancellationToken>()), Times.Never);

        _mockLogger.Verify(nameof(DeleteEmailCommandHandler),
            expectedLogLevel: LogLevel.Warning,
            times: Times.Once);
    }

    [Fact(DisplayName = nameof(DeleteEmailCommandHandler)
                        + nameof(DeleteEmailCommandHandler.Handle)
                        + "User Not Exists - Should Error")]
    public async Task Handle_EmailNotExists_ShouldReturnError()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        UserEntity userEntity = UserEntityBuilder
            .BuildSingle(totalIncludeEmail: 2);

        DeleteEmailCommand command = DeleteEmailCommandBuilder
            .BuildSingleInvalid();

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
        Result<VoidResponse> result = await _handler
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
            .And.BeEquivalentTo(DomainErrors.Email.NotFound);

        _mockReadRepository.Verify(mock =>
            mock.GetByIdAsync(It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()), Times.Once);

        _mockWriteRepository.Verify(mock =>
            mock.UpdateAsync(It.IsAny<UserEntity>(),
                It.IsAny<CancellationToken>()), Times.Never);

        _mockLogger.Verify(nameof(DeleteEmailCommandHandler),
            expectedLogLevel: LogLevel.Warning,
            times: Times.Once);
    }

    #endregion
}
