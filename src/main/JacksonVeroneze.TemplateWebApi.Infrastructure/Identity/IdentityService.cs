using System.Security.Claims;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Identity;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Infra;
using Microsoft.AspNetCore.Http;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Identity;

[ExcludeFromCodeCoverage]
public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<UserIdentity> GetAsync()
    {
        ClaimsPrincipal? user = _httpContextAccessor.HttpContext?.User;

        string? name = user!.Identity?.Name;

        string? id = user.FindFirstValue("userId");

        UserIdentity identity = new()
        {
            Id = Guid.Parse(id!),
            Name = name!
        };

        return Task.FromResult(identity);
    }
}
