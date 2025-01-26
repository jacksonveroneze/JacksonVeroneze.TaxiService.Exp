using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.Application.v1.Commands.User;

public sealed record InactivateUserCommand(Guid Id) : IRequest<Result>;