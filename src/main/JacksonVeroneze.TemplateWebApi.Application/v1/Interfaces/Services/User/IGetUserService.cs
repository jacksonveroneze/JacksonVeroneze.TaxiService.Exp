using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Services.User;

public interface IGetUserService
{
    Task<Result<UserEntity>> TryGetUserAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
