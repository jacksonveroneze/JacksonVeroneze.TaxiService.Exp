using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries.Bank;

public record GetBankByIdQuery : IRequest<BaseResponse>
{
    [FromRoute(Name = "id")]
    public Guid? Id { get; init; }
}
