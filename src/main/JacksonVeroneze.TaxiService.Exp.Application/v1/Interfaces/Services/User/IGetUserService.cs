using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Services.User;

public interface IGetUserService
{
    Task<Result<UserEntity>> TryGetUserAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
