using JacksonVeroneze.TemplateWebApi.Domain.Entities.Old;
using JacksonVeroneze.TemplateWebApi.Domain.Filters.Old;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Old;

public interface IStateDistribCachedRepository : IStateRepository
{
    Task<StateEntity?> GetByIdAsync(
        StateByIdFilter filter,
        CancellationToken cancellationToken = default);
}
