using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;

public sealed record AcceptRideCommand :
    IRequest<IResult<VoidResponse>>
{
    [FromRoute(Name = "rideId")]
    public Guid Id { get; init; }

    [FromRoute(Name = "driverId")]
    public Guid DriverId { get; init; }
}
