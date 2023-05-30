using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;

public interface IStateRepository
{
    Task<ICollection<StateEntity>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<StateEntity?> GetByIdAsync(
        StateByIdFilter filter,
        CancellationToken cancellationToken = default);
}