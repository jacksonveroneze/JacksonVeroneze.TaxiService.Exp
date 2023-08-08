using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Results.Old.State;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries.Bank;

public record GetBankPagedQuery : PagedQuery, IRequest<BaseResponse>
{
    private const string DefaultorderBy = nameof(StateResult.Id);

    private const SortDirection DefaultOrder = SortDirection.Ascending;

    public GetBankPagedQuery() : base(DefaultorderBy, DefaultOrder)
    {
    }
}
