using JacksonVeroneze.NET.Pagination.Offset;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;
using JacksonVeroneze.TaxiService.Exp.Domain.Filters;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Email;

public interface IEmailReadRepository
{
    public Task<bool> ExistsAsync(
        string email,
        CancellationToken cancellationToken = default);

    public Task<EmailEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    public Task<Page<EmailEntity>> GetPagedAsync(
        EmailPagedFilter filter,
        CancellationToken cancellationToken = default);
}