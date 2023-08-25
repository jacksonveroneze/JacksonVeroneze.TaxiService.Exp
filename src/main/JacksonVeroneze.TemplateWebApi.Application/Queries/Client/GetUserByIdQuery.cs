using JacksonVeroneze.TemplateWebApi.Application.Models.User;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries.Client;

public record GetUserByIdQuery : IRequest<IResult<GetUserByIdQueryResponse>>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; init; }
}
