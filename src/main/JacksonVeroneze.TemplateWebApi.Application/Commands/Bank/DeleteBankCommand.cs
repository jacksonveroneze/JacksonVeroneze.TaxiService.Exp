using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;

public record DeleteBankCommand : IRequest<Result<BaseResponse>>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; init; }
}
