using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Results.City;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;

public interface ICityPaginatedRepository
{
    Task<Page<City>> GetByStateIdPageAsync(
        CityByStateFilter filter,
        CancellationToken cancellationToken = default);
}