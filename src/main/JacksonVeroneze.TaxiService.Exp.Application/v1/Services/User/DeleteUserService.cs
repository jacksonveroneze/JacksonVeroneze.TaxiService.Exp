using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Services.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Services.User;

public sealed class DeleteUserService(
    ILogger<DeleteUserService> logger,
    IUserReadRepository readRepository,
    IUserWriteRepository writeRepository)
    : IDeleteUserService
{
    public async Task<Result> DeleteAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userId);

        UserEntity? entity = await readRepository
            .GetByIdAsync(userId, cancellationToken);

        if (entity is null)
        {
            return Result.FromInvalid(
                DomainErrors.User.NotFound);
        }

        await writeRepository.DeleteAsync(
            entity, cancellationToken);

        logger.LogDeleted(nameof(DeleteUserService),
            nameof(DeleteAsync), userId);

        return Result.WithSuccess();
    }
}
