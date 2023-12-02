using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;

public sealed record InactivateUserCommand :
    IRequest<Result<VoidResponse>>
{
    [FromRoute(Name = "userId")]
    public Guid Id { get; init; }
}
