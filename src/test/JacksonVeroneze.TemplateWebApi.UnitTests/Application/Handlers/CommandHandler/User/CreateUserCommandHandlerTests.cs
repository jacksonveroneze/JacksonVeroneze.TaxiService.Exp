using AutoMapper;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Mappers;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.User;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.AutoMapper;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Commands.User;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Extensions;
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

        _mockLogger.MockLogLevel();

        IMapper mapper = AutoMapperBuilder.Build<UserMapper>();

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
                mock.ExistsUserAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Callback((string document, CancellationToken _) =>
            {
                document.Should()
                    .NotBeNullOrEmpty()
                    .And.Be(command.Document);
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

        UserResponse? userResponse =
            result.Value!.Data;

        userResponse.Should()
            .NotBeNull();

        userResponse!.Id.Should()
            .NotBeNull();

        userResponse.Name.Should()
            .Be(command.Name);

        userResponse.Birthday.Should()
            .Be(command.Birthday);

        userResponse.Gender.Should()
            .Be(command.Gender);

        userResponse.Status.Should()
            .Be(UserStatus.PendingActivation);

        result.Errors.Should()
            .BeNullOrEmpty();

        _mockReadRepository.Verify(mock =>
            mock.ExistsUserAsync(It.IsAny<string>(),
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
            .BuildInvalidSingle();

        _mockReadRepository.Setup(mock =>
                mock.ExistsUserAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Callback((string document, CancellationToken _) =>
            {
                document.Should()
                    .Be(command.Document);
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
            .And.BeEquivalentTo(DomainErrors.User.DuplicateCpf);

        _mockReadRepository.Verify(mock =>
            mock.ExistsUserAsync(It.IsAny<string>(),
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
                mock.ExistsUserAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Callback((string document, CancellationToken _) =>
            {
                document.Should()
                    .Be(command.Document);
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
            mock.ExistsUserAsync(It.IsAny<string>(),
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
