using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Services.User;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Services.User;

public sealed class DeleteUserService(
    ILogger<DeleteUserService> logger,
    IUserReadRepository readRepository,
    IUserWriteRepository writeRepository)
    : IDeleteUserService
{
    public async Task<IResult> DeleteAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userId);

        UserEntity? entity = await readRepository
            .GetByIdAsync(userId, cancellationToken);

        if (entity is null)
        {
            return Result.Invalid(
                DomainErrors.User.NotFound);
        }

        await writeRepository.DeleteAsync(
            entity, cancellationToken);

        logger.LogDeleted(nameof(DeleteUserService),
            nameof(DeleteAsync), userId);

        return Result.Success();
    }
}
