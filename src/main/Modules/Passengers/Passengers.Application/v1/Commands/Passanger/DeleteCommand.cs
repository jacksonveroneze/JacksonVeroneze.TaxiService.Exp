using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.Application.v1.Commands.Passanger;

public sealed record DeleteCommand(Guid Id) : IRequest<Result>;