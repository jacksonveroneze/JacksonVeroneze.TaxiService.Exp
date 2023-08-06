using System.Security.Claims;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Identity;
using JacksonVeroneze.TemplateWebApi.Application.Models.Infra;
using Microsoft.AspNetCore.Http;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<UserIdentity> GetAsync()
    {
        string? name = _httpContextAccessor.HttpContext?.User.Identity?.Name;

        string? id = _httpContextAccessor?.HttpContext?.User.FindFirstValue("userId");

        UserIdentity identity = new() { Id = Guid.Parse(id!), Name = name! };

        return Task.FromResult(identity);
    }
}
