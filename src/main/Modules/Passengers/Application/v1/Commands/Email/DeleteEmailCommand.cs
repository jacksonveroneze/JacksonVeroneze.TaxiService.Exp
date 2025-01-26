using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.Application.v1.Commands.Email;

public sealed record DeleteEmailCommand(Guid Id, Guid EmailId)
    : IRequest<Result>;