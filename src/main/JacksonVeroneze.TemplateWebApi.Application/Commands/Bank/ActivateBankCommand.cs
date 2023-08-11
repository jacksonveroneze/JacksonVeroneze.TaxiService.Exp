using JacksonVeroneze.TemplateWebApi.Application.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;

public record ActivateBankCommand : IRequest<Result>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; init; }
}
