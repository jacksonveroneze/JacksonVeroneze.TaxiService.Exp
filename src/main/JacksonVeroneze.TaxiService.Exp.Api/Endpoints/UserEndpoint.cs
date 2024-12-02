using Asp.Versioning.Builder;
using Result = JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User.Email;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Api.Endpoints;

internal static class UserEndpoint
{
    public static WebApplication AddUserEndpoints(
        this WebApplication app)
    {
        ApiVersionSet apiVersionSet = app.ApiVersion();

        RouteGroupBuilder builder =
            app.MapGroup("/api/v{version:apiVersion}/users")
                .WithTags("users")
                .WithApiVersionSet(apiVersionSet)
                .MapToApiVersion(1);

        builder.MapGet(string.Empty, async (
                [FromServices] IMediator mediator,
                [AsParameters] GetUserPagedQuery query,
                CancellationToken cancellationToken) =>
            {
                Result.Result<GetUserPagedQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.MatchGet();
            })
            .Produces<GetUserPagedQueryResponse>()
            .AddDefaultResponseEndpoints();

        builder.MapGet("{id:guid:required}", async (
                [FromServices] IMediator mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                GetUserByIdQuery query = new(id);

                Result.Result<GetUserByIdQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.MatchGetById();
            })
            .Produces<GetUserByIdQueryResponse>()
            .AddDefaultResponseEndpoints();

        builder.MapPost(string.Empty, async (
                [FromServices] IMediator mediator,
                [FromBody] CreateUserCommand command,
                CancellationToken cancellationToken) =>
            {
                Result.Result<CreateUserCommandResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPost();
            })
            .Produces<CreateUserCommandResponse>()
            .AddDefaultResponseEndpoints();

        builder.MapDelete("{id:guid:required}", async (
                [FromServices] IMediator mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                DeleteUserCommand command = new(id);

                Result.Result response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchDelete();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        builder.MapPut("{id:guid:required}/activate", async (
                [FromServices] IMediator mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                ActivateUserCommand command = new(id);

                Result.Result response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPut();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        builder.MapPut("{id:guid:required}/inactivate", async (
                [FromServices] IMediator mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                InactivateUserCommand command = new(id);

                Result.Result response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPut();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        #region emails

        builder.MapGet("{id:guid:required}/emails", async (
                [FromServices] IMediator mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                GetEmailsByUserIdPagedQuery pagedQuery = new(id);

                Result.Result<GetEmailsByUserIdQueryResponse> response =
                    await mediator.Send(pagedQuery, cancellationToken);

                return response.MatchGet();
            })
            .Produces<GetEmailsByUserIdQueryResponse>()
            .AddDefaultResponseEndpoints();

        builder.MapPost("{id:guid:required}/emails", async (
                [FromServices] IMediator mediator,
                [AsParameters] CreateEmailCommand command,
                CancellationToken cancellationToken) =>
            {
                Result.Result<CreateEmailCommandResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPost();
            })
            .Produces<CreateEmailCommandResponse>()
            .AddDefaultResponseEndpoints();

        builder.MapDelete("{id:guid:required}/emails/{emailId:guid:required}", async (
                [FromServices] IMediator mediator,
                [FromRoute] Guid id,
                [FromRoute] Guid emailId,
                CancellationToken cancellationToken) =>
            {
                DeleteEmailCommand command = new(id, emailId);

                Result.Result response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPut();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        #endregion

        return app;
    }
}