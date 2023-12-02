using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Identity;

namespace JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Identity;

public interface IIdentityService
{
    Task<UserIdentity> GetAsync();
}
