using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.User.Email;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Queries.User.Email;

public sealed record GetAllEmailsByUserIdQuery :
    IRequest<IResult<GetAllEmailsByUserIdQueryResponse>>
{
    [FromRoute(Name = "userId")]
    public Guid Id { get; init; }
}
