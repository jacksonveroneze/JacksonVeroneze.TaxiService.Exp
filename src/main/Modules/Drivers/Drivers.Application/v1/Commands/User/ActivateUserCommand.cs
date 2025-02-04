using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Passengers.Application.v1.Commands.User;

public sealed record ActivateUserCommand(Guid Id) : IRequest<Result>;