using JacksonVeroneze.TemplateWebApi.Application.Models.Client;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries.Client;

public record GetClientByIdQuery : IRequest<IResult<GetClientByIdQueryResponse>>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; init; }
}
