using JacksonVeroneze.TemplateWebApi.Application.Models.Account;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries.Account;

public record GetAccountByIdQuery : IRequest<IResult<GetAccountByIdQueryResponse>>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; init; }
}
