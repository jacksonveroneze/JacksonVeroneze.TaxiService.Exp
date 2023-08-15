using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;

public record DeleteBankCommand : IRequest<IResult<VoidResponse>>
{
    [FromRoute(Name = "id")] public Guid Id { get; init; }
}
