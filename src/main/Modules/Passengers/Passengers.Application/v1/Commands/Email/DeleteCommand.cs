using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.Application.v1.Commands.Email;

public sealed record DeleteCommand(Guid Id, Guid EmailId)
    : IRequest<Result>;