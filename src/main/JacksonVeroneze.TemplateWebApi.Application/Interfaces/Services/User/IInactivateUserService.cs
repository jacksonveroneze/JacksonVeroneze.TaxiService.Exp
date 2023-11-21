using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services.User;

public interface IInactivateUserService
{
    Task<IResult> InactivateAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}
