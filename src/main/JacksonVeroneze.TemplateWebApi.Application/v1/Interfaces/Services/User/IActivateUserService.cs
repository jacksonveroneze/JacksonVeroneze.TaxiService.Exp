using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Services.User;

public interface IActivateUserService
{
    Task<Result> ActivateAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}
