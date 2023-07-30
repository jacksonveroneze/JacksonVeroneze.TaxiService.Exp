using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response.Pagination;

namespace JacksonVeroneze.TemplateWebApi.Application.Models.State;

public record GetStatePagedQueryResponse : PagedResponse<StateResponse>
{
}
