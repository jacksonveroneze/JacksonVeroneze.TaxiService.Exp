using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Commands.User;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.User;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.User;

public sealed class DeleteCommandHandler(
    ILogger<DeleteCommandHandler> logger,
    IUserReadRepository userReadRepository,
    IUserWriteRepository userWriteRepository)
    : IRequestHandler<DeleteCommand, Result>
{
    public async Task<Result> Handle(
        DeleteCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<UserEntity> resultEntity = await GetUserByIdAsync(
            request.Id, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return resultEntity;
        }

        await DeleteAsync(resultEntity.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private async Task<Result<UserEntity>> GetUserByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        UserEntity? entity = await userReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<UserEntity>.WithSuccess(entity);
        }

        Error error = DomainErrors.UserError.NotFound;

        logger.LogNotFound(
            nameof(DeleteCommandHandler),
            nameof(GetUserByIdAsync), id, error);

        return Result<UserEntity>.FromNotFound(error);
    }

    private async Task DeleteAsync(
        UserEntity user,
        CancellationToken cancellationToken)
    {
        await userWriteRepository.DeleteAsync(
            user, cancellationToken);

        logger.LogDeleted(nameof(DeleteCommandHandler),
            nameof(Handle), user.Id);
    }
}