using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;

public record DeleteBankCommand : IRequest<BaseResponse>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; init; }
}
