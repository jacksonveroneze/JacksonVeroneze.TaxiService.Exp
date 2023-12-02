using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Ride;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Queries.Ride;

public sealed record GetRideByIdQuery :
    IRequest<Result<GetRideByIdQueryResponse>>
{
    [FromRoute(Name = "rideId")]
    public Guid Id { get; init; }
}
