using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;

public record CreateBankCommand : IRequest<BaseResponse>
{
    [FromRoute(Name = "name")]
    public string? Name { get; init; }
}
