using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response.Pagination;

namespace JacksonVeroneze.TemplateWebApi.Application.Models.Client;

public sealed record GetClientPagedQueryResponse : PagedResponse<ClientResponse>
{
}
