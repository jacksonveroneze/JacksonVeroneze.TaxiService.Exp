using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Email;

public interface IEmailWriteRepository
{
    Task CreateAsync(
        EmailEntity entity,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        EmailEntity entity,
        CancellationToken cancellationToken = default);
}