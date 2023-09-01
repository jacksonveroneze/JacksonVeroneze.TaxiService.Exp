using JacksonVeroneze.TemplateWebApi.Application.Handlers.CommandHandler.User;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Common;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Application.Handlers.CommandHandler.User;

public class InactivateUserCommandHandlerTests
{
    private readonly Mock<ILogger<InactivateUserCommandHandler>> _mockLogger;
    private readonly Mock<IUserReadRepository> _mockReadRepository;
    private readonly Mock<IUserWriteRepository> _mockWriteRepository;
    private readonly Mock<IDateTime> _mockDateTime;

    private readonly InactivateUserCommandHandler _handler;

    public InactivateUserCommandHandlerTests()
    {
        _mockLogger = new Mock<ILogger<InactivateUserCommandHandler>>();
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

        _handler = new InactivateUserCommandHandler(
            _mockLogger.Object,
            _mockReadRepository.Object,
            _mockWriteRepository.Object,
            _mockDateTime.Object
        );
    }

    #region success

    [Fact(DisplayName = nameof(InactivateUserCommandHandler)
                        + nameof(InactivateUserCommandHandler.Handle)
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

    [Fact(DisplayName = nameof(InactivateUserCommandHandler)
                        + nameof(InactivateUserCommandHandler.Handle)
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

    [Fact(DisplayName = nameof(InactivateUserCommandHandler)
                        + nameof(InactivateUserCommandHandler.Handle)
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
