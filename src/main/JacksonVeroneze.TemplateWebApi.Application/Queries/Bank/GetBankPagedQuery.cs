using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Models.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Primitives;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries.Bank;

public class GetBankPagedQuery : PagedQuery, IRequest<IResult<BaseResponse>>
{
    private const string DefaultOrderBy = nameof(BankResponse.Id);

    private const SortDirection DefaultOrder = SortDirection.Ascending;

    [FromQuery(Name = "name")]
    public string? Name { get; init; }

    [FromQuery(Name = "status")]
    public BankStatus? Status { get; init; }

    public GetBankPagedQuery() : base(DefaultOrderBy, DefaultOrder)
    {
    }
}
