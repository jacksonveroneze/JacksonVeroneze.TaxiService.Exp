using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;

public interface IUserReadDistribCachedRepository
{
    Task<UserEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}