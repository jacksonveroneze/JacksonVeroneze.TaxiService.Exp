using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Services.User;

public interface IInactivateUserService
{
    Task<Result> InactivateAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}
