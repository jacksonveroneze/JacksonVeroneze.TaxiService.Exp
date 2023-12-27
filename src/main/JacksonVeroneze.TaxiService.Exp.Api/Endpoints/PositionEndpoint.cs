using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Position;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace JacksonVeroneze.TaxiService.Exp.Api.Endpoints;

public static class PositionEndpoint
{
    public static WebApplication AddPositionEndpoints(
        this WebApplication app)
    {
        RouteGroupBuilder builder =
            app.MapGroup("/api/v1/positions")
                .WithTags("positions")
                .AddFluentValidationAutoValidation();

        builder.MapPost(string.Empty, async (
                [FromServices] IMediator mediator,
                [FromBody] UpdatePositionCommand command,
                CancellationToken cancellationToken) =>
            {
                Result<VoidResponse> response = await mediator
                    .Send(command, cancellationToken);

                return response.MatchPost();
            })
            .AddDefaultResponseEndpoints()
            .Produces(StatusCodes.Status204NoContent);

        return app;
    }
}