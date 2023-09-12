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

public class UsersControllerTests : IDisposable,
    IClassFixture<CustomWebApplicationFactory<Program>>
{
    private const string BaseUrl = "/api/v1/users";

    private readonly HttpClient _httpClient;

    public UsersControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient(new
            WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
    }

    #region GetPagedAsync

    [Fact(DisplayName = nameof(UsersController)
                        + nameof(UsersController.GetPagedAsync)
                        + "Should Success", Skip = "")]
    public async Task GetPagedAsync_ReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        int page = 1;
        int pageSize = 10;

        Uri uri = new($"{BaseUrl}?page={page}&page_size={pageSize}",
            UriKind.Relative);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        HttpResponseMessage response = await _httpClient
            .GetAsync(uri);

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

        // content.Data.Should()
        //     .NotBeNull()
        //     .And.HaveCount(0);
    }

    #endregion

    #region CreateAsync

    [Fact(DisplayName = nameof(UsersController)
                        + nameof(UsersController.CreateAsync)
                        + "Should Success", Skip = "")]
    public async Task CreateAsync_ReturnSuccess()
    {
        // -------------------------------------------------------
        // Arrange
        // -------------------------------------------------------
        CreateUserCommand command = CreateUserCommandBuilder
            .BuildSingle();

        Uri uri = new(BaseUrl, UriKind.Relative);

        // -------------------------------------------------------
        // Act
        // -------------------------------------------------------
        HttpResponseMessage response = await _httpClient
            .PostAsJsonAsync(uri, command);

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

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
