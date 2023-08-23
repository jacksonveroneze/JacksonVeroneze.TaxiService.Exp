using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;

public sealed record InactivateBankCommand : IRequest<IResult<VoidResponse>>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; init; }
}
