using AutoMapper;
using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.QueryHandler.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Mappers;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Queries.User;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.AutoMapper;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Queries.User;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Extensions;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TemplateWebApi.UnitTests.Application.Handlers.QueryHandler.User;

public class GetUserPagedQueryHandlerTests
{
    private readonly GetUserPagedQueryHandler _handler;
    private readonly Mock<ILogger<GetUserPagedQueryHandler>> _mockLogger;
    private readonly Mock<IUserReadRepository> _mockReadRepository;

    public GetUserPagedQueryHandlerTests()
    {
        _mockLogger = new Mock<ILogger<GetUserPagedQueryHandler>>();
        _mockReadRepository = new Mock<IUserReadRepository>();

        _mockLogger.MockLogLevel();

        IMapper mapper = AutoMapperBuilder.Build<UserMapper>();

        _handler = new GetUserPagedQueryHandler(
            _mockLogger.Object,
            mapper,
            _mockReadRepository.Object
        );
    }

    #region success

    [Fact(DisplayName = nameof(GetUserPagedQueryHandler)
                        + nameof(GetUserPagedQueryHandler.Handle)
                        + "Should Success")]
    public async Task Handle_ReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        GetUserPagedQuery command = GetUserPagedQueryBuilder
            .BuildSingle();

        Page<UserEntity> page = UserEntityBuilder.BuildPagedSingle(2);

        _mockReadRepository.Setup(mock =>
                mock.GetPagedAsync(
                    It.IsAny<UserPagedFilter>(),
                    It.IsAny<CancellationToken>()))
            .Callback((UserPagedFilter filter, CancellationToken _) =>
            {
                filter.Should()
                    .NotBeNull();
            })
            .ReturnsAsync(page);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        IResult<GetUserPagedQueryResponse> result = await _handler
            .Handle(command, CancellationToken.None);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        result.Should()
            .NotBeNull();

        result.IsSuccess.Should()
            .BeTrue();

        result.Value!.Pagination.Should()
            .NotBeNull()
            .And.BeEquivalentTo(page.Pagination,
                opt => opt.ExcludingMissingMembers());

        result.Value!.Data.Should()
            .NotBeNull();

        result.Value!.Data.Should().SatisfyRespectively(
            first =>
            {
                first.Id.Should().Be(page.Data.First().Id);
                first.Name.Should().Be(page.Data.First().Name);
                first.Birthday.Should().Be(page.Data.First().Birthday);
                first.Gender.Should().Be(page.Data.First().GenderType);
                first.Status.Should().Be(page.Data.First().Status);
            },
            second =>
            {
                second.Id.Should().Be(page.Data.Last().Id);
                second.Name.Should().Be(page.Data.Last().Name);
                second.Birthday.Should().Be(page.Data.Last().Birthday);
                second.Gender.Should().Be(page.Data.Last().GenderType);
                second.Status.Should().Be(page.Data.Last().Status);
            });

        result.Value!.Data.Should()
            .NotBeNullOrEmpty()
            .And.HaveSameCount(page.Data);

        result.Errors.Should()
            .BeNullOrEmpty();

        _mockReadRepository.Verify(mock =>
            mock.GetPagedAsync(It.IsAny<UserPagedFilter>(),
                It.IsAny<CancellationToken>()), Times.Once);

        _mockLogger.Verify(nameof(GetUserPagedQueryHandler),
            expectedLogLevel: LogLevel.Information,
            times: Times.Once);
    }

    #endregion
}
