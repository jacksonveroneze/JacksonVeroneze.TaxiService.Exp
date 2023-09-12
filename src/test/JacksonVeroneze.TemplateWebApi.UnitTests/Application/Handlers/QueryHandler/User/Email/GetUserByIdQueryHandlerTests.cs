using AutoMapper;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.QueryHandler.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.v1.Mappers;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.v1.Queries.User.Email;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.AutoMapper;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Queries.User.Email;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Extensions;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Application.Handlers.QueryHandler.User.Email;

public class GetAllEmailsByUserIdQueryHandlerTests
{
    private readonly GetAllEmailsByUserIdQueryHandler _handler;
    private readonly Mock<ILogger<GetAllEmailsByUserIdQueryHandler>> _mockLogger;
    private readonly Mock<IUserReadRepository> _mockReadRepository;

    public GetAllEmailsByUserIdQueryHandlerTests()
    {
        _mockLogger = new Mock<ILogger<GetAllEmailsByUserIdQueryHandler>>();
        _mockReadRepository = new Mock<IUserReadRepository>();

        _mockLogger.MockLogLevel();

        IMapper mapper = AutoMapperBuilder.Build<EmailMapper>();

        _handler = new GetAllEmailsByUserIdQueryHandler(
            _mockLogger.Object,
            mapper,
            _mockReadRepository.Object
        );
    }

    #region success

    [Fact(DisplayName = nameof(GetAllEmailsByUserIdQueryHandler)
                        + nameof(GetAllEmailsByUserIdQueryHandler.Handle)
                        + "Should Success")]
    public async Task Handle_ReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        GetAllEmailsByUserIdQuery command =
            GetAllEmailsByUserIdQueryBuilder.BuildSingle();

        UserEntity userEntity = UserEntityBuilder.BuildSingle(2);

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
        IResult<GetAllEmailsByUserIdQueryResponse> result = await _handler
            .Handle(command, CancellationToken.None);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeTrue();

        result.Value!.Data.Should()
            .NotBeNullOrEmpty()
            .And.HaveSameCount(userEntity.Emails);

        result.Errors.Should()
            .BeNullOrEmpty();

        _mockReadRepository.Verify(mock =>
            mock.GetByIdAsync(It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()), Times.Once);

        _mockLogger.Verify(nameof(GetAllEmailsByUserIdQueryHandler),
            expectedLogLevel: LogLevel.Information,
            times: Times.Once);
    }

    #endregion

    #region error

    [Fact(DisplayName = nameof(GetAllEmailsByUserIdQueryHandler)
                        + nameof(GetAllEmailsByUserIdQueryHandler.Handle)
                        + "User Not Exists - Should Error")]
    public async Task Handle_UserNotExists_ShouldReturnError()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        GetAllEmailsByUserIdQuery command =
            GetAllEmailsByUserIdQueryBuilder.BuildSingle();

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
        IResult<GetAllEmailsByUserIdQueryResponse> result = await _handler
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

        _mockLogger.Verify(nameof(GetAllEmailsByUserIdQueryHandler),
            expectedLogLevel: LogLevel.Warning,
            times: Times.Once);
    }

    #endregion
}
