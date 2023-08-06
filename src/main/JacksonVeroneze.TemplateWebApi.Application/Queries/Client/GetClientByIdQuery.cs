using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries.Client;

public record GetClientByIdQuery : IRequest<BaseResponse>
{
    [FromRoute(Name = "id")]
    public Guid? Id { get; init; }
}
