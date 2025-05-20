using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Passengers.Application.v1.Commands.Email;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Models.Email;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.Email;

public sealed class CreateCommandHandler(
    ILogger<CreateCommandHandler> logger,
    IDataMapper mapper,
    IPassengerReadRepository passengerReadRepository,
    IEmailReadRepository emailReadRepository,
    IEmailWriteRepository emailWriteRepository)
    : IRequestHandler<CreateCommand, Result<CreateCommandResponse>>
{
    public async Task<Result<CreateCommandResponse>> Handle(
        CreateCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<PassengerEntity> resultPassenger = await GetPassengerByIdAsync(
            request.PassengerId, cancellationToken);

        if (resultPassenger.IsFailure)
        {
            return Result<CreateCommandResponse>.FromNotFound(
                resultPassenger.FirstError!);
        }

        Result existsEmail = await ExistsEmailAsync(
            request, cancellationToken);

        if (existsEmail.IsFailure)
        {
            return Result<CreateCommandResponse>
                .FromInvalid(existsEmail.FirstError!);
        }

        Result<EmailEntity> entity = FactoryEntity(
            request, resultPassenger.Value!);

        if (entity.IsFailure)
        {
            return Result<CreateCommandResponse>
                .FromInvalid(entity.Errors!);
        }

        await CreateAsync(entity.Value!, cancellationToken);

        CreateCommandResponse response =
            mapper.Map<CreateCommandResponse>(entity.Value);

        return Result<CreateCommandResponse>.WithSuccess(response);
    }

    private Result<EmailEntity> FactoryEntity(
        CreateCommand request, PassengerEntity passenger)
    {
        Result<EmailEntity> entity = EmailEntity
            .Create(passenger.Id, request.Body!.Email);

        if (!entity.IsFailure)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(CreateCommandHandler),
            nameof(FactoryEntity), entity.Errors!.Count);

        return Result<EmailEntity>
            .FromInvalid(entity.Errors!);
    }

    private async Task<Result<PassengerEntity>> GetPassengerByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        PassengerEntity? passenger = await passengerReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (passenger is null)
        {
            return Result<PassengerEntity>.FromNotFound(
                DomainErrors.PassengerError.NotFound);
        }

        return Result<PassengerEntity>.WithSuccess(passenger);
    }

    private async Task<Result> ExistsEmailAsync(
        CreateCommand request,
        CancellationToken cancellationToken)
    {
        bool existsEmail = await emailReadRepository
            .ExistsAsync(request.Body!.Email!, cancellationToken);

        if (!existsEmail)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.EmailError.DuplicateEmail;

        logger.LogGenericError(
            nameof(CreateCommandHandler),
            nameof(Handle), request.PassengerId, error);

        return Result<CreateCommandResponse>
            .FromInvalid(error);
    }

    private async Task CreateAsync(
        EmailEntity email,
        CancellationToken cancellationToken)
    {
        await emailWriteRepository.CreateAsync(
            email, cancellationToken);

        logger.LogProcessed(
            nameof(CreateCommandHandler),
            nameof(CreateAsync), email.Id);
    }
}
