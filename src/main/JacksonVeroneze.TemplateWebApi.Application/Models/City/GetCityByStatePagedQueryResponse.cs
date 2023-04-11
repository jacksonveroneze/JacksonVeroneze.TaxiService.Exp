using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response.Pagination;
using JacksonVeroneze.TemplateWebApi.Domain.Results.City;

namespace JacksonVeroneze.TemplateWebApi.Application.Models.City;

public record GetCityByStatePagedQueryResponse : PagedResponse<CityResult>
{
}