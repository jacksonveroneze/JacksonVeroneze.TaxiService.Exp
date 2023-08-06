using JacksonVeroneze.TemplateWebApi.Application.Models.Infra;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Identity;

public interface IIdentityService
{
    Task<UserIdentity> GetAsync();
}
