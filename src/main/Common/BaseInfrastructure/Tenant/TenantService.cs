using Microsoft.AspNetCore.Http;
using TaxiService.Common.SharedKernel.Interfaces;

namespace TaxiService.Common.BaseInfrastructure.Tenant;

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
