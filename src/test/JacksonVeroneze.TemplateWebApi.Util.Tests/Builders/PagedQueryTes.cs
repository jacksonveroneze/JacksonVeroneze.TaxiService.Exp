using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Base;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders;

public record PagedQueryTes : PagedQuery
{
    public PagedQueryTes(string defaultOrderBy,
        SortDirection defaultOrder) :
        base(defaultOrderBy, defaultOrder)
    {
    }
}
