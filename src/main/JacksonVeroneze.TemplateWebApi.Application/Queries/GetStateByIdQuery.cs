using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries;

public record GetStateByIdQuery : IRequest<BaseResponse>
{
    [FromRoute(Name = "id")]
    public string? Id { get; set; }
}