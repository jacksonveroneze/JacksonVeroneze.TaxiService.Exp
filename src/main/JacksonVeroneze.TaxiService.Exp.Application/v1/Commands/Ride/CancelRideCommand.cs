using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Ride;

public sealed record CancelRideCommand(Guid Id) :
    IRequest<Result<VoidResponse>>;