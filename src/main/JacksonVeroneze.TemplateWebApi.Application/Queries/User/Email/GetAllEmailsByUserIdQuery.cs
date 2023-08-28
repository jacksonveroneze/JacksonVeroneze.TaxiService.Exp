using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.TemplateWebApi.Application.Models.User.Email;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries.User.Email;

public sealed record GetAllEmailsByUserIdQuery : IRequest<IResult<GetAllEmailsByUserIdQueryResponse>>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; init; }
}
