using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;

public sealed record ActivateUserCommand(Guid Id) : IRequest<Result>;