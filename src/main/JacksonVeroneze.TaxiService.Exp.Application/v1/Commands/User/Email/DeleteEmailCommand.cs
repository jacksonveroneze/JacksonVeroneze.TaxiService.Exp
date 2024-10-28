using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User.Email;

public sealed record DeleteEmailCommand(Guid Id, Guid EmailId) : IRequest<Result>;