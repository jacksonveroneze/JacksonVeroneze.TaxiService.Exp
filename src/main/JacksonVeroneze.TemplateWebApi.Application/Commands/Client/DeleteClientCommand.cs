using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Commands.Client;

public record DeleteClientCommand : IRequest<IResult<VoidResponse>>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; init; }
}
