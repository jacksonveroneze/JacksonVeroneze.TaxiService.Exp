using JacksonVeroneze.TemplateWebApi.Application.Handlers.CommandHandler.User;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Common;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Application.Handlers.CommandHandler.User;

public class DeleteUserCommandHandlerTests
{
    private readonly Mock<ILogger<DeleteUserCommandHandler>> _mockLogger;
    private readonly Mock<IUserReadRepository> _mockReadRepository;
    private readonly Mock<IUserWriteRepository> _mockWriteRepository;

    private readonly DeleteUserCommandHandler _handler;

    public DeleteUserCommandHandlerTests()
    {
        _mockLogger = new Mock<ILogger<DeleteUserCommandHandler>>();
        _mockReadRepository = new Mock<IUserReadRepository>();
        _mockWriteRepository = new Mock<IUserWriteRepository>();

        _mockLogger
            .Setup(mock => mock.IsEnabled(LogLevel.Information))
            .Returns(true);

        _mockLogger
            .Setup(mock => mock.IsEnabled(LogLevel.Warning))
            .Returns(true);

        _mockLogger
            .Setup(mock => mock.IsEnabled(LogLevel.Error))
            .Returns(true);

        _handler = new DeleteUserCommandHandler(
            _mockLogger.Object,
            _mockReadRepository.Object,
            _mockWriteRepository.Object
        );
    }

    #region success

    [Fact(DisplayName = nameof(DeleteUserCommandHandler)
                        + nameof(DeleteUserCommandHandler.Handle)
                        + "Should Success")]
    public async Task Handle_ReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
    }

    #endregion

    #region error

    [Fact(DisplayName = nameof(DeleteUserCommandHandler)
                        + nameof(DeleteUserCommandHandler.Handle)
                        + "User Not Exists - Should Error")]
    public async Task Handle_UserNotExists_ShouldReturnError()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
    }

    #endregion
}
