using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Request.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Domain.Results.Old.State;

namespace JacksonVeroneze.TemplateWebApi.Application.Queries.Old.State;

public class GetStatePagedQuery : PagedRequest, IRequest<BaseResponse>
{
    private const string DefaultorderBy = nameof(StateResult.Id);

    private const SortDirection DefaultOrder = SortDirection.Ascending;

    public GetStatePagedQuery() : base(DefaultorderBy, DefaultOrder)
    {
    }
}
