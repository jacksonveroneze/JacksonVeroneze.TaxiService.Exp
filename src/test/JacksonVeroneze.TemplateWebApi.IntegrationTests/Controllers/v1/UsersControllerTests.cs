using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using JacksonVeroneze.TemplateWebApi.Api.Controllers.v1;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.User;
using JacksonVeroneze.TemplateWebApi.IntegrationTests.Util;
using JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.Commands.User;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace JacksonVeroneze.TemplateWebApi.IntegrationTests.Controllers.v1;

public class UsersControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public UsersControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;

        // DbContext context = _factory.Services
        //     .GetRequiredService<ApplicationDbContext>();
        //
        // context.Database.EnsureCreated();
    }

    #region GetPagedAsync

    [Fact(DisplayName = nameof(UsersController)
                        + nameof(UsersController.GetPagedAsync)
                        + "Should Success", Skip = "Skipped")]
    public async Task GetPagedAsync_ReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        HttpClient client = _factory.CreateClient(new
            WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        HttpResponseMessage response = await client
            .GetAsync("/api/v1/users?page=1&page_size=2");

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        response.StatusCode.Should()
            .Be(HttpStatusCode.OK);

        GetUserPagedQueryResponse? content = await response.Content
            .ReadFromJsonAsync<GetUserPagedQueryResponse>(
                JsonSerializerConfig.Options);

        content.Should()
            .NotBeNull();

        content!.Pagination.Should()
            .NotBeNull();

        content.Data.Should()
            .NotBeNull()
            .And.HaveCount(2);
    }

    #endregion

    #region CreateAsync

    [Fact(DisplayName = nameof(UsersController)
                        + nameof(UsersController.CreateAsync)
                        + "Should Success", Skip = "Skipped")]
    public async Task CreateAsync_ReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        HttpClient client = _factory.CreateClient(new
            WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        CreateUserCommand command = CreateUserCommandBuilder
            .BuildSingle();

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        HttpResponseMessage response = await client
            .PostAsJsonAsync("/api/v1/users", command);

        // -------------------------------------------------------
        // Assert
        // -------------------------------------------------------
        response.Should()
            .NotBeNull();

        response.StatusCode.Should()
            .Be(HttpStatusCode.Created);

        CreateUserCommandResponse? content = await response.Content
            .ReadFromJsonAsync<CreateUserCommandResponse>(
                JsonSerializerConfig.Options);

        content.Should()
            .NotBeNull();

        content!.Data.Should()
            .NotBeNull()
            .And.BeEquivalentTo(command, op => op.ExcludingMissingMembers());
    }

    #endregion
}
