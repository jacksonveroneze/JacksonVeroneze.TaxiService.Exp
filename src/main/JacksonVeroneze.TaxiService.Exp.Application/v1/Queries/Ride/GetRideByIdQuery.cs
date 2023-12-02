using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Ride;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.Ride;

public sealed record GetRideByIdQuery :
    IRequest<Result<GetRideByIdQueryResponse>>
{
    [FromRoute(Name = "rideId")]
    public Guid Id { get; init; }
}
