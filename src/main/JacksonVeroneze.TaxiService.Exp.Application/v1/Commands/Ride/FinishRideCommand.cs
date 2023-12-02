using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Ride;

public sealed record FinishRideCommand :
    IRequest<Result<VoidResponse>>
{
    [FromRoute(Name = "rideId")]
    public Guid Id { get; init; }
}
