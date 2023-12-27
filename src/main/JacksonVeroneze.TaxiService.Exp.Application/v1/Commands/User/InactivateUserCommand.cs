using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;

public sealed record InactivateUserCommand(Guid Id) :
    IRequest<Result<VoidResponse>>;