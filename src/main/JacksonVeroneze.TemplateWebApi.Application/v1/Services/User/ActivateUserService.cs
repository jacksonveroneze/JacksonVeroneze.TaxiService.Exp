using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.System;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Services.User;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Services.User;

public sealed class ActivateUserService(
    ILogger<ActivateUserService> logger,
    IUserReadRepository readRepository,
    IUserWriteRepository writeRepository,
    IDateTime dateTime)
    : IActivateUserService
{
    public async Task<Result> ActivateAsync(
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

        Result result = entity.Activate(dateTime.UtcNow);

        if (result.IsFailure)
        {
            logger.LogAlreadyProcessed(nameof(ActivateUserService),
                nameof(ActivateAsync), userId, result.Error!);

            return Result.FromInvalid(result.Error!);
        }

        await writeRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(nameof(ActivateUserService),
            nameof(ActivateAsync), userId);

        return Result.WithSuccess();
    }
}
