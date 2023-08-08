using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Results.Old.State;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries.Client;

public record GetClientPagedQuery : PagedQuery, IRequest<BaseResponse>
{
    private const string DefaultorderBy = nameof(StateResult.Id);

    private const SortDirection DefaultOrder = SortDirection.Ascending;

    public GetClientPagedQuery() : base(DefaultorderBy, DefaultOrder)
    {
    }
}
