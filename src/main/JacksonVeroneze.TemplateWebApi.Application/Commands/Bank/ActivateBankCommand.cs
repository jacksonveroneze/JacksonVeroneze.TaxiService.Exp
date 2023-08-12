using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;

public sealed record ActivateBankCommand : IRequest<Result<BaseResponse>>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; init; }
}
