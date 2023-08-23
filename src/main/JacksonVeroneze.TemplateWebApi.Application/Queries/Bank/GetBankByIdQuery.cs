using JacksonVeroneze.TemplateWebApi.Application.Models.Bank;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries.Bank;

public record GetBankByIdQuery : IRequest<IResult<GetBankByIdQueryResponse>>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; init; }
}
