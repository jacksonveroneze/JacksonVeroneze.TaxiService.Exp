using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response.Pagination;

namespace JacksonVeroneze.TemplateWebApi.Application.Models.City;

public record GetCityByStatePagedQueryResponse : PagedResponse<CityResponse>
{
}
