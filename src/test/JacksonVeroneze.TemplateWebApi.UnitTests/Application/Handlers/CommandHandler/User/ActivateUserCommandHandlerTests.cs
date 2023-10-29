// using JacksonVeroneze.NET.Result;
// using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
// using JacksonVeroneze.TemplateWebApi.Application.Interfaces.System;
// using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User;
// using JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.User;
// using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
// using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
// using JacksonVeroneze.TemplateWebApi.Domain.Entities;
// using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Commands.User;
// using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.Entities;
// using JacksonVeroneze.TemplateWebApi.Util.Tests.Extensions;
// using Microsoft.Extensions.Logging;
//
// namespace JacksonVeroneze.TemplateWebApi.UnitTests.Application.Handlers.CommandHandler.User;
//
// public class ActivateUserCommandHandlerTests
// {
//     private readonly ActivateUserCommandHandler _handler;
//     private readonly Mock<IDateTime> _mockDateTime;
//     private readonly Mock<ILogger<ActivateUserCommandHandler>> _mockLogger;
//     private readonly Mock<IUserReadRepository> _mockReadRepository;
//     private readonly Mock<IUserWriteRepository> _mockWriteRepository;
//
//     public ActivateUserCommandHandlerTests()
//     {
//         _mockLogger = new Mock<ILogger<ActivateUserCommandHandler>>();
//         _mockReadRepository = new Mock<IUserReadRepository>();
//         _mockWriteRepository = new Mock<IUserWriteRepository>();
//         _mockDateTime = new Mock<IDateTime>();
//
//         _mockLogger.MockLogLevel();
//
//         _handler = new ActivateUserCommandHandler(
//             _mockLogger.Object,
//             _mockReadRepository.Object,
//             _mockWriteRepository.Object,
//             _mockDateTime.Object
//         );
//     }
//
//     #region success
//
//     [Fact(DisplayName = nameof(ActivateUserCommandHandler)
//                         + nameof(ActivateUserCommandHandler.Handle)
//                         + "Should Success")]
//     public async Task Handle_ReturnSuccess()
//     {
//         // -------------------------------------------------------
//         // Arrange
//         // -------------------------------------------------------
//         ActivateUserCommand command = ActivateUserCommandBuilder
//             .BuildSingle();
//
//         UserEntity userEntity = UserEntityBuilder.BuildSingle();
//
//         _mockReadRepository.Setup(mock =>
//                 mock.GetByIdAsync(
//                     It.IsAny<Guid>(),
//                     It.IsAny<CancellationToken>()))
//             .Callback((Guid id, CancellationToken _) =>
//             {
//                 id.Should()
//                     .Be(command.Id);
//             })
//             .ReturnsAsync(userEntity);
//
//         _mockDateTime.SetupGet(mock =>
//             mock.UtcNow).Returns(DateTime.UtcNow);
//
//         // -------------------------------------------------------
//         // Act
//         // -------------------------------------------------------
//         IResult<VoidResponse> result = await _handler
//             .Handle(command, CancellationToken.None);
//
//         // -------------------------------------------------------
//         // Assert
//         // -------------------------------------------------------
//         result.Should()
//             .NotBeNull();
//
//         result.IsSuccess.Should()
//             .BeTrue();
//
//         result.Errors.Should()
//             .BeNullOrEmpty();
//
//         _mockReadRepository.Verify(mock =>
//             mock.GetByIdAsync(It.IsAny<Guid>(),
//                 It.IsAny<CancellationToken>()), Times.Once);
//
//         _mockWriteRepository.Verify(mock =>
//             mock.UpdateAsync(It.IsAny<UserEntity>(),
//                 It.IsAny<CancellationToken>()), Times.Once);
//
//         _mockLogger.Verify(nameof(ActivateUserCommandHandler),
//             expectedLogLevel: LogLevel.Information,
//             times: Times.Once);
//     }
//
//     #endregion
//
//     #region error
//
//     [Fact(DisplayName = nameof(ActivateUserCommandHandler)
//                         + nameof(ActivateUserCommandHandler.Handle)
//                         + "User Not Exists - Should Error")]
//     public async Task Handle_UserNotExists_ShouldReturnError()
//     {
//         // -------------------------------------------------------
//         // Arrange
//         // -------------------------------------------------------
//         ActivateUserCommand command = ActivateUserCommandBuilder
//             .BuildSingle();
//
//         UserEntity? userEntity = null;
//
//         _mockReadRepository.Setup(mock =>
//                 mock.GetByIdAsync(
//                     It.IsAny<Guid>(),
//                     It.IsAny<CancellationToken>()))
//             .Callback((Guid id, CancellationToken _) =>
//             {
//                 id.Should()
//                     .Be(command.Id);
//             })
//             .ReturnsAsync(userEntity);
//
//         // -------------------------------------------------------
//         // Act
//         // -------------------------------------------------------
//         IResult<VoidResponse> result = await _handler
//             .Handle(command, CancellationToken.None);
//
//         // -------------------------------------------------------
//         // Assert
//         // -------------------------------------------------------
//         result.Should()
//             .NotBeNull();
//
//         result.IsSuccess.Should()
//             .BeFalse();
//
//         result.Error.Should()
//             .NotBeNull()
//             .And.BeEquivalentTo(DomainErrors.User.NotFound);
//
//         _mockReadRepository.Verify(mock =>
//             mock.GetByIdAsync(It.IsAny<Guid>(),
//                 It.IsAny<CancellationToken>()), Times.Once);
//
//         _mockWriteRepository.Verify(mock =>
//             mock.UpdateAsync(It.IsAny<UserEntity>(),
//                 It.IsAny<CancellationToken>()), Times.Never);
//
//         _mockLogger.Verify(nameof(ActivateUserCommandHandler),
//             expectedLogLevel: LogLevel.Warning,
//             times: Times.Once);
//     }
//
//     [Fact(DisplayName = nameof(ActivateUserCommandHandler)
//                         + nameof(ActivateUserCommandHandler.Handle)
//                         + "Invalid Data - Should Error")]
//     public async Task Handle_InvalidData_ShouldReturnError()
//     {
//         // -------------------------------------------------------
//         // Arrange
//         // -------------------------------------------------------
//         ActivateUserCommand command = ActivateUserCommandBuilder
//             .BuildSingle();
//
//         UserEntity userEntity = UserEntityBuilder.BuildSingle();
//
//         userEntity.Activate(DateTime.UtcNow);
//
//         userEntity.ClearEvents();
//
//         _mockReadRepository.Setup(mock =>
//                 mock.GetByIdAsync(
//                     It.IsAny<Guid>(),
//                     It.IsAny<CancellationToken>()))
//             .Callback((Guid id, CancellationToken _) =>
//             {
//                 id.Should()
//                     .Be(command.Id);
//             })
//             .ReturnsAsync(userEntity);
//
//         _mockDateTime.SetupGet(mock =>
//             mock.UtcNow).Returns(DateTime.UtcNow);
//
//         // -------------------------------------------------------
//         // Act
//         // -------------------------------------------------------
//         IResult<VoidResponse> result = await _handler
//             .Handle(command, CancellationToken.None);
//
//         // -------------------------------------------------------
//         // Assert
//         // -------------------------------------------------------
//         result.Should()
//             .NotBeNull();
//
//         result.IsSuccess.Should()
//             .BeFalse();
//
//         result.Error.Should()
//             .NotBeNull()
//             .And.BeEquivalentTo(DomainErrors.User.AlreadyActivated);
//
//         _mockReadRepository.Verify(mock =>
//             mock.GetByIdAsync(It.IsAny<Guid>(),
//                 It.IsAny<CancellationToken>()), Times.Once);
//
//         _mockWriteRepository.Verify(mock =>
//             mock.UpdateAsync(It.IsAny<UserEntity>(),
//                 It.IsAny<CancellationToken>()), Times.Never);
//
//         _mockLogger.Verify(nameof(ActivateUserCommandHandler),
//             expectedLogLevel: LogLevel.Error,
//             times: Times.Once);
//     }
//
//     #endregion
// }



