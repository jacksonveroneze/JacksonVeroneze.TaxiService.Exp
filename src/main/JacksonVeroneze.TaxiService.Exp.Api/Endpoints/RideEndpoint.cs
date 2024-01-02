using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.Ride;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace JacksonVeroneze.TaxiService.Exp.Api.Endpoints;

public static class RideEndpoint
{
    public static WebApplication AddRideEndpoints(
        this WebApplication app)
    {
        RouteGroupBuilder builder =
            app.MapGroup("/api/v1/rides")
                .WithTags("rides")
                .AddFluentValidationAutoValidation();

        builder.MapGet(string.Empty, async (
                [FromServices] IMediator mediator,
                [AsParameters] GetRidePagedQuery query,
                CancellationToken cancellationToken) =>
            {
                Result<GetRidePagedQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.MatchGet();
            })
            .Produces<GetRidePagedQueryResponse>()
            .AddDefaultResponseEndpoints();

        builder.MapGet("{id:guid:required}", async (
                [FromServices] IMediator mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                GetRideByIdQuery query = new(id);

                Result<GetRideByIdQueryResponse> response =
                    await mediator.Send(query, cancellationToken);

                return response.MatchGetById();
            })
            .Produces<GetRideByIdQueryResponse>()
            .AddDefaultResponseEndpoints();

        builder.MapPost(string.Empty, async (
                [FromServices] IMediator mediator,
                [FromBody] RequestRideCommand command,
                CancellationToken cancellationToken) =>
            {
                Result<RequestRideCommandResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPost();
            })
            .Produces<CreateUserCommandResponse>()
            .AddDefaultResponseEndpoints();

        builder.MapPut("{id:guid:required}/accept", async (
                [FromServices] IMediator mediator,
                [AsParameters] AcceptRideCommand command,
                CancellationToken cancellationToken) =>
            {
                Result<VoidResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPut();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        builder.MapPut("{id:guid:required}/start", async (
                [FromServices] IMediator mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                StartRideCommand command = new(id);

                Result<VoidResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPut();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        builder.MapPut("{id:guid:required}/finish", async (
                [FromServices] IMediator mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                FinishRideCommand command = new(id);

                Result<VoidResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPut();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        builder.MapPut("{id:guid:required}/cancel", async (
                [FromServices] IMediator mediator,
                [FromRoute] Guid id,
                CancellationToken cancellationToken) =>
            {
                CancelRideCommand command = new(id);

                Result<VoidResponse> response =
                    await mediator.Send(command, cancellationToken);

                return response.MatchPut();
            })
            .Produces(StatusCodes.Status204NoContent)
            .AddDefaultResponseEndpoints();

        return app;
    }
}