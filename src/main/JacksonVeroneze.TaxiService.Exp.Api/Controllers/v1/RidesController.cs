using System.Net.Mime;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Api.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.Ride;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}/rides")]
[Produces(MediaTypeNames.Application.Json)]
public sealed class RidesController : ControllerBase
{
    private readonly ISender _mediator;

    public RidesController(
        ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Get))]
    public async Task<IActionResult> GetPagedAsync(
        GetRidePagedQuery query,
        CancellationToken cancellationToken)
    {
        Result<GetRidePagedQueryResponse> response = await _mediator
            .Send(query, cancellationToken);

        return response.MatchGet(this);
    }

    [HttpGet("{rideId:required}")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Find))]
    public async Task<ActionResult<GetRideByIdQueryResponse>> GetByIdAsync(
        GetRideByIdQuery query,
        CancellationToken cancellationToken)
    {
        Result<GetRideByIdQueryResponse> response =
            await _mediator.Send(query, cancellationToken);

        return response.MatchFind(this);
    }

    [HttpPost]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Create))]
    public async Task<IActionResult> CreateAsync(
        [FromBody] RequestRideCommand command,
        CancellationToken cancellationToken)
    {
        Result<RequestRideCommandResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchPost(this);
    }

    [HttpPut("{rideId:required}/accept")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Put))]
    public async Task<IActionResult> AcceptAsync(
        AcceptRideCommand command,
        CancellationToken cancellationToken)
    {
        Result<VoidResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchPut(this);
    }

    [HttpPut("{rideId:required}/start")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Put))]
    public async Task<IActionResult> StartAsync(
        StartRideCommand command,
        CancellationToken cancellationToken)
    {
        Result<VoidResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchPut(this);
    }

    [HttpPut("{rideId:required}/finish")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Put))]
    public async Task<IActionResult> FinishAsync(
        FinishRideCommand command,
        CancellationToken cancellationToken)
    {
        Result<VoidResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchPut(this);
    }

    [HttpPut("{rideId:required}/cancel")]
    [ApiConventionMethod(typeof(DefaultApiConventions),
        nameof(DefaultApiConventions.Put))]
    public async Task<IActionResult> CancelAsync(
        CancelRideCommand command,
        CancellationToken cancellationToken)
    {
        Result<VoidResponse> response = await _mediator
            .Send(command, cancellationToken);

        return response.MatchPut(this);
    }
}
