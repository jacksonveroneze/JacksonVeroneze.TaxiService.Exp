using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Ride;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;

public sealed record RequestRideCommand :
    IRequest<IResult<RequestRideCommandResponse>>
{
}
