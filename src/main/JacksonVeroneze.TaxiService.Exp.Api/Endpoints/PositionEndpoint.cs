using Asp.Versioning.Builder;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Api.Endpoints.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Position;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace JacksonVeroneze.TaxiService.Exp.Api.Endpoints;

internal static class PositionEndpoint
{
    public static WebApplication AddPositionEndpoints(
        this WebApplication app)
    {
        ApiVersionSet apiVersionSet = app.ApiVersion();

        RouteGroupBuilder builder =
            app.MapGroup("/api/v{version:apiVersion}/positions")
                .WithTags("positions")
                .WithApiVersionSet(apiVersionSet)
                .MapToApiVersion(1)
                .AddFluentValidationAutoValidation();

        builder.MapPost(string.Empty, async (
                [FromServices] IMediator mediator,
                [FromBody] AddPositionCommand command,
                CancellationToken cancellationToken) =>
            {
                Result response = await mediator
                    .Send(command, cancellationToken);

                return response.MatchPost();
            })
            .AddDefaultResponseEndpoints()
            .Produces(StatusCodes.Status204NoContent);

        return app;
    }
}