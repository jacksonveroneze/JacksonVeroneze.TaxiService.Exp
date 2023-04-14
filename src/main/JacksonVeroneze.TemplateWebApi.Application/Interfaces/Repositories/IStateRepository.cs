using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Results.State;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;

public interface IStateRepository
{
    Task<ICollection<StateResult>?> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<StateResult?> GetByIdAsync(
        StateByIdFilter filter,
        CancellationToken cancellationToken = default);
}