using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.System;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Services.User;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Services.User;

public sealed class InactivateUserService(
    ILogger<InactivateUserService> logger,
    IUserReadRepository readRepository,
    IUserWriteRepository writeRepository,
    IDateTime dateTime)
    : IInactivateUserService
{
    public async Task<Result> InactivateAsync(
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

        Result result = entity.Inactivate(dateTime.UtcNow);

        if (result.IsFailure)
        {
            logger.LogAlreadyProcessed(nameof(InactivateUserService),
                nameof(InactivateAsync), userId, result.Error!);

            return Result.FromInvalid(result.Error!);
        }

        await writeRepository.UpdateAsync(
            entity, cancellationToken);

        logger.LogProcessed(nameof(InactivateUserService),
            nameof(InactivateAsync), userId);

        return Result.WithSuccess();
    }
}
