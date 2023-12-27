using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Tenant;
using Microsoft.AspNetCore.Http;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Tenant;

[ExcludeFromCodeCoverage]
public class TenantService(
    IHttpContextAccessor httpContextAccessor) : ITenantService
{
    private const string TenantIdHeaderName = "X-TenantId";

    public Guid TenantId
    {
        get
        {
            string? tenant = httpContextAccessor
                .HttpContext?
                .Request
                .Headers[TenantIdHeaderName];

            ArgumentException.ThrowIfNullOrEmpty(tenant);

            return Guid.Parse(tenant);
        }
    }
}