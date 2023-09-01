using JacksonVeroneze.TemplateWebApi.Application.Handlers.CommandHandler.User;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Common;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Application.Handlers.CommandHandler.User;

public class ActivateUserCommandHandlerTests
{
    private readonly Mock<ILogger<ActivateUserCommandHandler>> _mockLogger;
    private readonly Mock<IUserReadRepository> _mockReadRepository;
    private readonly Mock<IUserWriteRepository> _mockWriteRepository;
    private readonly Mock<IDateTime> _mockDateTime;

    private readonly ActivateUserCommandHandler _handler;

    public ActivateUserCommandHandlerTests()
    {
        _mockLogger = new Mock<ILogger<ActivateUserCommandHandler>>();
        _mockReadRepository = new Mock<IUserReadRepository>();
        _mockWriteRepository = new Mock<IUserWriteRepository>();
        _mockDateTime = new Mock<IDateTime>();

        _mockLogger
            .Setup(mock => mock.IsEnabled(LogLevel.Information))
            .Returns(true);

        _mockLogger
            .Setup(mock => mock.IsEnabled(LogLevel.Warning))
            .Returns(true);

        _mockLogger
            .Setup(mock => mock.IsEnabled(LogLevel.Error))
            .Returns(true);

        _handler = new ActivateUserCommandHandler(
            _mockLogger.Object,
            _mockReadRepository.Object,
            _mockWriteRepository.Object,
            _mockDateTime.Object
        );
    }

    #region success

    [Fact(DisplayName = nameof(ActivateUserCommandHandler)
                        + nameof(ActivateUserCommandHandler.Handle)
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

    [Fact(DisplayName = nameof(ActivateUserCommandHandler)
                        + nameof(ActivateUserCommandHandler.Handle)
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

    [Fact(DisplayName = nameof(ActivateUserCommandHandler)
                        + nameof(ActivateUserCommandHandler.Handle)
                        + "Invalid Data - Should Error")]
    public async Task Handle_InvalidData_ShouldReturnError()
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
