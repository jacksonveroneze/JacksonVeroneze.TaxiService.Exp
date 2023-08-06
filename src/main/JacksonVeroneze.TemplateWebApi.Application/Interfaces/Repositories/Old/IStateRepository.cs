using JacksonVeroneze.TemplateWebApi.Domain.Entities.Old;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Old;

public interface IStateRepository
{
    Task<ICollection<StateEntity>> GetAllAsync(
        CancellationToken cancellationToken = default);
}
