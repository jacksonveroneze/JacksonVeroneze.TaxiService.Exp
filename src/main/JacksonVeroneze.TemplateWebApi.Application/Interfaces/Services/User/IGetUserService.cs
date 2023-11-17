using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services.User;

public interface IGetUserService
{
    Task<IResult<UserEntity>> TryGetUserAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
