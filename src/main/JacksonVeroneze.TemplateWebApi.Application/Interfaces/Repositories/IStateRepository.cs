using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;

public interface IStateRepository
{
    Task<ICollection<StateEntity>> GetAllAsync(
        CancellationToken cancellationToken = default);
}
