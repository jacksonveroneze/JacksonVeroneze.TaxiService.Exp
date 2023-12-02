using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User.Email;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User.Email;

public sealed record GetAllEmailsByUserIdQuery :
    IRequest<Result<GetAllEmailsByUserIdQueryResponse>>
{
    [FromRoute(Name = "userId")]
    public Guid Id { get; init; }
}
