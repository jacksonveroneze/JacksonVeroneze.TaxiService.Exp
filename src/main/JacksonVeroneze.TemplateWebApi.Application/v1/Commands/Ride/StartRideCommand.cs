using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;

public sealed record StartRideCommand :
    IRequest<Result<VoidResponse>>
{
    [FromRoute(Name = "rideId")]
    public Guid Id { get; init; }
}
