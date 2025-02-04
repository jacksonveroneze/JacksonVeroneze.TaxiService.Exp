using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.Application.v1.Commands.User;

public sealed record DeleteUserCommand(Guid Id) : IRequest<Result>;