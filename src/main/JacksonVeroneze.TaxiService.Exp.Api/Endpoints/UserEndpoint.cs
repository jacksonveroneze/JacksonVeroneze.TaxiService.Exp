using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User.Email;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace JacksonVeroneze.TaxiService.Exp.Api.Endpoints;

public static class UserEndpoint
{
    public static WebApplication AddUserEndpoints(
        this WebApplication app)
    {
        RouteGroupBuilder builder =
            app.MapGroup("/api/v1/users")
                .WithTags("users")
                .AddFluentValidationAutoValidation();

        builder.MapGet(string.Empty, async (
                [FromServices] IMediator mediator,
                [AsParameters] GetUserPagedQuery query,
                CancellationToken cancellationToken) =>
            {
                Result<GetUserPagedQueryResponse> response =
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

                Result<GetUserByIdQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.MatchGetById();
            })
            .Produces<GetUserByIdQueryResponse>()
            .AddDefaultResponseEndpoints();

        builder.MapPost("/", async (
                [FromServices] IMediator mediator,
                [FromBody] CreateUserCommand command,
                CancellationToken cancellationToken) =>
            {
                Result<CreateUserCommandResponse> response =
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

                Result<VoidResponse> response =
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

                Result<VoidResponse> response =
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

                Result<VoidResponse> response =
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

                Result<GetEmailsByUserIdPagedQueryResponse> response =
                    await mediator.Send(pagedQuery, cancellationToken);

                return response.MatchGet();
            })
            .Produces<GetEmailsByUserIdPagedQueryResponse>()
            .AddDefaultResponseEndpoints();

        builder.MapPost("{id:guid:required}/emails", async (
                [FromServices] IMediator mediator,
                [AsParameters] CreateEmailCommand command,
                CancellationToken cancellationToken) =>
            {
                Result<CreateEmailCommandResponse> response =
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

                Result<VoidResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPut();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        #endregion

        return app;
    }
}