using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Email;

public interface IEmailWriteRepository
{
    public Task CreateAsync(
        EmailEntity entity,
        CancellationToken cancellationToken = default);

    public Task DeleteAsync(
        EmailEntity entity,
        CancellationToken cancellationToken = default);
}