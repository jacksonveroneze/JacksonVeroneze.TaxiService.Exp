using System.Security.Claims;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Identity;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Identity;
using Microsoft.AspNetCore.Http;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Identity;

[ExcludeFromCodeCoverage]
public class IdentityService(
    IHttpContextAccessor httpContextAccessor) : IIdentityService
{
    public Task<UserIdentity> GetAsync()
    {
        ClaimsPrincipal? user = httpContextAccessor.HttpContext?.User;

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
