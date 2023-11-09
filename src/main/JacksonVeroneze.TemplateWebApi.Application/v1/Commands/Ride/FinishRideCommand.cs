using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;

public sealed record FinishRideCommand :
    IRequest<IResult<VoidResponse>>
{
    [FromRoute(Name = "rideId")]
    public Guid Id { get; init; }
}
