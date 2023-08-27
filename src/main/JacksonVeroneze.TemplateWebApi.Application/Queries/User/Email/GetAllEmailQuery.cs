using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.TemplateWebApi.Application.Models.User.Email;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries.User.Email;

public sealed record GetAllEmailQuery : IRequest<IResult<GetAllEmailQueryResponse>>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; init; }
}
