using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Identity;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Identity;

public interface IIdentityService
{
    Task<UserIdentity> GetAsync();
}
