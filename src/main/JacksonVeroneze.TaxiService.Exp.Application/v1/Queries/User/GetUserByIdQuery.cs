using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Queries.User;

public sealed record GetUserByIdQuery :
    IRequest<Result<GetUserByIdQueryResponse>>
{
    [FromRoute(Name = "userId")]
    public Guid Id { get; init; }
}
