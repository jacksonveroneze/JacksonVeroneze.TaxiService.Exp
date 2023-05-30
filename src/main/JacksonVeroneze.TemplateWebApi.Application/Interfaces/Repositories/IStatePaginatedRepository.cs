using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Results.State;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;

public interface IStatePaginatedRepository
{
    Task<Page<State>> GetAllAsync(
        StateAllFilter filter,
        CancellationToken cancellationToken = default);
}