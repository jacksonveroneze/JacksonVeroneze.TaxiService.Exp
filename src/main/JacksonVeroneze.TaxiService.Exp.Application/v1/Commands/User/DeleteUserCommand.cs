using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;

public sealed record DeleteUserCommand(Guid Id) :
    IRequest<Result<VoidResponse>>;