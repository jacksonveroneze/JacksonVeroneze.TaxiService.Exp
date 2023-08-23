using JacksonVeroneze.TemplateWebApi.Application.Models.Client;
using JacksonVeroneze.TemplateWebApi.Application.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries.Client;

public record GetClientByIdQuery : IRequest<IResult<GetClientByIdQueryResponse>>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; init; }
}
