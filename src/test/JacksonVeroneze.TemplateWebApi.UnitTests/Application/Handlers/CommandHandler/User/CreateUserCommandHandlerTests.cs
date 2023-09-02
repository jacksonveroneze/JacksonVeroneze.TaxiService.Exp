using AutoMapper;
using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.TemplateWebApi.Application.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.Handlers.CommandHandler.User;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.Mappers;
using JacksonVeroneze.TemplateWebApi.Application.Models.User;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Commands.User;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Util;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Application.Handlers.CommandHandler.User;

public class CreateUserCommandHandlerTests
{
    private readonly Mock<ILogger<CreateUserCommandHandler>> _mockLogger;
    private readonly Mock<IUserReadRepository> _mockReadRepository;
    private readonly Mock<IUserWriteRepository> _mockWriteRepository;

    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _mockLogger = new Mock<ILogger<CreateUserCommandHandler>>();
        _mockReadRepository = new Mock<IUserReadRepository>();
        _mockWriteRepository = new Mock<IUserWriteRepository>();

        IMapper mapper = AutoMapperBuilder.Build<UserMapper>();

        _mockLogger
            .Setup(mock => mock.IsEnabled(LogLevel.Information))
            .Returns(true);

        _mockLogger
            .Setup(mock => mock.IsEnabled(LogLevel.Warning))
            .Returns(true);

        _mockLogger
            .Setup(mock => mock.IsEnabled(LogLevel.Error))
            .Returns(true);

        _handler = new CreateUserCommandHandler(
            _mockLogger.Object,
            mapper,
            _mockReadRepository.Object,
            _mockWriteRepository.Object
        );
    }

    #region success

    [Fact(DisplayName = nameof(CreateUserCommandHandler)
                        + nameof(CreateUserCommandHandler.Handle)
                        + "Should Success")]
    public async Task Handle_ReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        CreateUserCommand command = CreateUserCommandBuilder
            .BuildSingle();

        _mockReadRepository.Setup(mock =>
                mock.ExistsByNameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Callback((string name, CancellationToken _) =>
            {
                name.Should()
                    .NotBeNullOrEmpty()
                    .And.Be(command.Name);
            })
            .ReturnsAsync(false);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult<CreateUserCommandResponse> result = await _handler
            .Handle(command, CancellationToken.None);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeTrue();

        result.Value.Should()
            .NotBeNull();

        _mockReadRepository.Verify(mock =>
            mock.ExistsByNameAsync(It.IsAny<string>(),
                It.IsAny<CancellationToken>()), Times.Once);

        _mockWriteRepository.Verify(mock =>
            mock.CreateAsync(It.IsAny<UserEntity>(),
                It.IsAny<CancellationToken>()), Times.Once);

        _mockLogger.Verify(nameof(CreateUserCommandHandler),
            times: Times.Once);
    }

    #endregion

    #region error

    [Fact(DisplayName = nameof(CreateUserCommandHandler)
                        + nameof(CreateUserCommandHandler.Handle)
                        + "User Exists - Should Error")]
    public async Task Handle_UserExists_ShouldReturnError()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        CreateUserCommand command = CreateUserCommandBuilder
            .BuildSingle();

        _mockReadRepository.Setup(mock =>
                mock.ExistsByNameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Callback((string name, CancellationToken _) =>
            {
                name.Should()
                    .NotBeNullOrEmpty()
                    .And.Be(command.Name);
            })
            .ReturnsAsync(true);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult<CreateUserCommandResponse> result = await _handler
            .Handle(command, CancellationToken.None);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeFalse();

        result.Value.Should()
            .BeNull();

        result.Error.Should()
            .NotBeNull()
            .And.BeEquivalentTo(DomainErrors.User.DuplicateName);

        _mockReadRepository.Verify(mock =>
            mock.ExistsByNameAsync(It.IsAny<string>(),
                It.IsAny<CancellationToken>()), Times.Once);

        _mockWriteRepository.Verify(mock =>
            mock.CreateAsync(It.IsAny<UserEntity>(),
                It.IsAny<CancellationToken>()), Times.Never);

        _mockLogger.Verify(nameof(CreateUserCommandHandler),
            expectedLogLevel: LogLevel.Warning,
            times: Times.Once);
    }

    [Fact(DisplayName = nameof(CreateUserCommandHandler)
                        + nameof(CreateUserCommandHandler.Handle)
                        + "Invalid Data - Should Error")]
    public async Task Handle_InvalidData_ShouldReturnError()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        CreateUserCommand command = CreateUserCommandBuilder
            .BuildInvalidSingle();

        _mockReadRepository.Setup(mock =>
                mock.ExistsByNameAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Callback((string name, CancellationToken _) =>
            {
                name.Should()
                    .NotBeNullOrEmpty()
                    .And.Be(command.Name);
            })
            .ReturnsAsync(false);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult<CreateUserCommandResponse> result = await _handler
            .Handle(command, CancellationToken.None);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeFalse();

        result.Value.Should()
            .BeNull();

        result.Errors.Should()
            .NotBeNullOrEmpty()
            .And.HaveCount(2)
            .And.Contain(x => x.Code == DomainErrors.User.InvalidName.Code
                              || x.Code == DomainErrors.User.InvalidCpf.Code);

        _mockReadRepository.Verify(mock =>
            mock.ExistsByNameAsync(It.IsAny<string>(),
                It.IsAny<CancellationToken>()), Times.Once);

        _mockWriteRepository.Verify(mock =>
            mock.CreateAsync(It.IsAny<UserEntity>(),
                It.IsAny<CancellationToken>()), Times.Never);

        _mockLogger.Verify(nameof(CreateUserCommandHandler),
            expectedLogLevel: LogLevel.Error,
            times: Times.Once);
    }

    #endregion
}
