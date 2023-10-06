using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Tenant;
using Microsoft.AspNetCore.Http;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Tenant;

[ExcludeFromCodeCoverage]
public class TenantService : ITenantService
{
    private const string TenantIdHeaderName = "X-TenantId";

    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid TenantId
    {
        get
        {
            string? tenant = _httpContextAccessor
                .HttpContext?
                .Request
                .Headers[TenantIdHeaderName];

            ArgumentException.ThrowIfNullOrEmpty(tenant);

            return Guid.Parse(tenant);
        }
    }
}
