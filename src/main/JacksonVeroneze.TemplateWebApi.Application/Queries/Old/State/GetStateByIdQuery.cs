using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries.Old.State;

public record GetStateByIdQuery : IRequest<BaseResponse>
{
    [FromRoute(Name = "id")]
    public string? Id { get; init; }
}
