using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Services.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Services.User;

public sealed class GetUserService(
    ILogger<GetUserService> logger,
    IUserReadRepository readRepository)
    : IGetUserService
{
    public async Task<Result<UserEntity>> TryGetUserAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        UserEntity? entity = await readRepository
            .GetByIdAsync(id, cancellationToken);

        return entity is not null
            ? Result<UserEntity>.WithSuccess(entity)
            : Result<UserEntity>.FromNotFound(DomainErrors.User.NotFound);
    }
}
