using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;

public interface IStateRepository
{
    Task<ICollection<State>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<State?> GetByIdAsync(
        StateByIdFilter filter,
        CancellationToken cancellationToken = default);
}