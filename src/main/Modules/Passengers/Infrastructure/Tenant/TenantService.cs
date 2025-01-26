using Microsoft.AspNetCore.Http;
using TaxiService.Modules.Passengers.Application.Interfaces.Tenant;

namespace TaxiService.Modules.Passengers.Infrastructure.Tenant;

[ExcludeFromCodeCoverage]
public class TenantService(
    IHttpContextAccessor httpContextAccessor) : ITenantService
{
    private const string TenantIdHeaderName = "X-TenantId";

    public Guid GetTenantId()
    {
        string? tenant = httpContextAccessor
            .HttpContext?
            .Request
            .Headers[TenantIdHeaderName];

        ArgumentException.ThrowIfNullOrEmpty(tenant);

        return Guid.Parse(tenant);
    }
}