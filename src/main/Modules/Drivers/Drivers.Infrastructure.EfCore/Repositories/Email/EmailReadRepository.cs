using JacksonVeroneze.NET.EntityFramework.Interfaces;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Infrastructure.EfCore.Contexts;

namespace TaxiService.Modules.Drivers.Infrastructure.EfCore.Repositories.Email;

[ExcludeFromCodeCoverage]
public class EmailReadRepository(
    IEfCoreRepository<EmailEntity, DriversDbContext> service)
    : IEmailReadRepository
{
    public async Task<bool> ExistsAsync(string email,
        CancellationToken cancellationToken)
    {
        bool exists = await service.AnyAsync(conf =>
                string.Equals(conf.Email.Value, email.ToLower()),
            cancellationToken);

        return exists;
    }
}