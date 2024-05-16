// using JacksonVeroneze.NET.Result;
// using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
// using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
// using JacksonVeroneze.TemplateWebApi.Application.Interfaces.System;
// using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
// using JacksonVeroneze.TemplateWebApi.Application.v1.Services.User;
// using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
// using JacksonVeroneze.TemplateWebApi.Domain.Entities;
// using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.Entities;
// using JacksonVeroneze.TemplateWebApi.Util.Tests.Extensions;
// using Microsoft.Extensions.Logging;
//
// namespace JacksonVeroneze.TemplateWebApi.UnitTests.Application.Services;
//
// public class ActivateUserServiceTests
// {
//     private readonly Mock<ILogger<ActivateUserService>> _mockLogger;
//     private readonly Mock<IUserReadRepository> _mockReadRepository;
//     private readonly Mock<IUserWriteRepository> _mockWriteRepository;
//     private readonly Mock<IIntegrationEventPublisher> _mockIntegrationEventPublisher;
//     private readonly Mock<IDateTime> _mockDateTime;
//
//     private readonly ActivateUserService _service;
//
//     public ActivateUserServiceTests()
//     {
//         _mockLogger = new Mock<ILogger<ActivateUserService>>();
//         _mockReadRepository = new Mock<IUserReadRepository>();
//         _mockWriteRepository = new Mock<IUserWriteRepository>();
//         _mockIntegrationEventPublisher = new Mock<IIntegrationEventPublisher>();
//         _mockDateTime = new Mock<IDateTime>();
//
//         _mockLogger.MockLogLevel();
//
//         _service = new ActivateUserService(
//             _mockLogger.Object,
//             _mockReadRepository.Object,
//             _mockWriteRepository.Object,
//             _mockIntegrationEventPublisher.Object,
//             _mockDateTime.Object
//         );
//     }
//
//
//     #region success
//
//     [Fact(DisplayName = nameof(ActivateUserService)
//                         + nameof(ActivateUserService.ActivateAsync)
//                         + "Should Success")]
//     public async Task Activate_ReturnSuccess()
//     {
//         // -------------------------------------------------------
//         // Arrange
//         // -------------------------------------------------------
//         Guid userId = Guid.NewGuid();
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
//                     .Be(userId);
//             })
//             .ReturnsAsync(userEntity);
//
//         _mockDateTime.SetupGet(mock =>
//             mock.UtcNow).Returns(DateTime.UtcNow);
//
//         // -------------------------------------------------------
//         // Act
//         // -------------------------------------------------------
//         Result<VoidResponse> result = await _service
//             .ActivateAsync(userId, CancellationToken.None);
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
//         _mockLogger.Verify(nameof(ActivateUserService),
//             expectedLogLevel: LogLevel.Information,
//             times: Times.Once);
//     }
//
//     #endregion
//
//     #region error
//
//     [Fact(DisplayName = nameof(ActivateUserService)
//                         + nameof(ActivateUserService.ActivateAsync)
//                         + "User Not Exists - Should Error")]
//     public async Task Activate_UserNotExists_ShouldReturnError()
//     {
//         // -------------------------------------------------------
//         // Arrange
//         // -------------------------------------------------------
//         Guid userId = Guid.NewGuid();
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
//                     .Be(userId);
//             })
//             .ReturnsAsync(userEntity);
//
//         // -------------------------------------------------------
//         // Act
//         // -------------------------------------------------------
//         Result<VoidResponse> result = await _service
//             .ActivateAsync(userId, CancellationToken.None);
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
//         _mockLogger.Verify(nameof(ActivateUserService),
//             expectedLogLevel: LogLevel.Warning,
//             times: Times.Once);
//     }
//
//     [Fact(DisplayName = nameof(ActivateUserService)
//                         + nameof(ActivateUserService.ActivateAsync)
//                         + "Invalid Data - Should Error")]
//     public async Task Activate_InvalidData_ShouldReturnError()
//     {
//         // -------------------------------------------------------
//         // Arrange
//         // -------------------------------------------------------
//         Guid userId = Guid.NewGuid();
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
//                     .Be(userId);
//             })
//             .ReturnsAsync(userEntity);
//
//         _mockDateTime.SetupGet(mock =>
//             mock.UtcNow).Returns(DateTime.UtcNow);
//
//         // -------------------------------------------------------
//         // Act
//         // -------------------------------------------------------
//         Result<VoidResponse> result = await _service
//             .ActivateAsync(userId, CancellationToken.None);
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
//         _mockLogger.Verify(nameof(ActivateUserService),
//             expectedLogLevel: LogLevel.Error,
//             times: Times.Once);
//     }
//
//     #endregion
// }