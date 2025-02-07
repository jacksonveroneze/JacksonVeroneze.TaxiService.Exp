using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.Application.v1.Commands.User;

public sealed record DeleteCommand(Guid Id) : IRequest<Result>;