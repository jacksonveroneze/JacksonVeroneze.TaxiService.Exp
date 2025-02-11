using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Commands.Passanger;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passanger;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.Passanger;

public sealed class DeleteCommandHandler(
    ILogger<DeleteCommandHandler> logger,
    IPassangerReadRepository passangerReadRepository,
    IPassangerWriteRepository passangerWriteRepository)
    : IRequestHandler<DeleteCommand, Result>
{
    public async Task<Result> Handle(
        DeleteCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<PassangerEntity> resultEntity = await GetByIdAsync(
            request.Id, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return resultEntity;
        }

        await DeleteAsync(resultEntity.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private async Task<Result<PassangerEntity>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        PassangerEntity? entity = await passangerReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            return Result<PassangerEntity>.WithSuccess(entity);
        }

        Error error = DomainErrors.PassangerError.NotFound;

        logger.LogNotFound(
            nameof(DeleteCommandHandler),
            nameof(GetByIdAsync), id, error);

        return Result<PassangerEntity>.FromNotFound(error);
    }

    private async Task DeleteAsync(
        PassangerEntity passanger,
        CancellationToken cancellationToken)
    {
        await passangerWriteRepository.DeleteAsync(
            passanger, cancellationToken);

        logger.LogDeleted(nameof(DeleteCommandHandler),
            nameof(Handle), passanger.Id);
    }
}