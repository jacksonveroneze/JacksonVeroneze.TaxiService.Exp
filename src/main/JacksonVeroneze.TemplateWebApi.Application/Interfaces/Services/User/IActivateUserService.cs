using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services.User;

public interface IActivateUserService
{
    Task<IResult> ActivateAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}
