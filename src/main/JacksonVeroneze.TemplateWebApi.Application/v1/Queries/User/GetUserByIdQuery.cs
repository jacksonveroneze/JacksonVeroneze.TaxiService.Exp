using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Queries.User;

public sealed record GetUserByIdQuery :
    IRequest<IResult<GetUserByIdQueryResponse>>
{
    [FromRoute(Name = "userId")]
    public Guid Id { get; init; }
}
