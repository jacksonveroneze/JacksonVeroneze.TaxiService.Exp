using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User.Email;

public sealed record DeleteEmailCommand(Guid Id, Guid EmailId) :
    IRequest<Result<VoidResponse>>;