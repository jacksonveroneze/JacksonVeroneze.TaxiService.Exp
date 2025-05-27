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

public sealed class CreatePassengerCommandHandler(
    ILogger<CreatePassengerCommandHandler> logger,
    IDataMapper mapper,
    IPassengerRepository passengerRepository,
    IEmailReadRepository emailReadRepository,
    IEmailWriteRepository emailWriteRepository)
    : IRequestHandler<CreatePassengerCommand, Result<CreateEmailCommandResponse>>
{
    public async Task<Result<CreateEmailCommandResponse>> Handle(
        CreatePassengerCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<PassengerEntity> resultPassenger = await GetPassengerByIdAsync(
            request.PassengerId, cancellationToken);

        if (resultPassenger.IsFailure)
        {
            return Result<CreateEmailCommandResponse>.FromNotFound(
                resultPassenger.FirstError!);
        }

        Result existsEmail = await ExistsEmailAsync(
            request, cancellationToken);

        if (existsEmail.IsFailure)
        {
            return Result<CreateEmailCommandResponse>
                .FromInvalid(existsEmail.FirstError!);
        }

        Result<EmailEntity> entity = FactoryEntity(
            request, resultPassenger.Value!);

        if (entity.IsFailure)
        {
            return Result<CreateEmailCommandResponse>
                .FromInvalid(entity.Errors);
        }

        await CreateAsync(entity.Value!, cancellationToken);

        CreateEmailCommandResponse response =
            mapper.Map<CreateEmailCommandResponse>(entity.Value);

        return Result<CreateEmailCommandResponse>.WithSuccess(response);
    }

    private Result<EmailEntity> FactoryEntity(
        CreatePassengerCommand request, PassengerEntity passenger)
    {
        Result<EmailEntity> entity = EmailEntity
            .Create(passenger.Id, request.Body!.Email);

        if (!entity.IsFailure)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(CreatePassengerCommandHandler),
            nameof(FactoryEntity), entity.Errors.Count);

        return Result<EmailEntity>
            .FromInvalid(entity.Errors);
    }

    private async Task<Result<PassengerEntity>> GetPassengerByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        PassengerEntity? passenger = await passengerRepository
            .GetByIdAsync(id, cancellationToken);

        if (passenger is null)
        {
            return Result<PassengerEntity>.FromNotFound(
                DomainErrors.PassengerError.NotFound);
        }

        return Result<PassengerEntity>.WithSuccess(passenger);
    }

    private async Task<Result> ExistsEmailAsync(
        CreatePassengerCommand request,
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
            nameof(CreatePassengerCommandHandler),
            nameof(Handle), request.PassengerId, error);

        return Result<CreateEmailCommandResponse>
            .FromInvalid(error);
    }

    private async Task CreateAsync(
        EmailEntity email,
        CancellationToken cancellationToken)
    {
        await emailWriteRepository.CreateAsync(
            email, cancellationToken);

        logger.LogProcessed(
            nameof(CreatePassengerCommandHandler),
            nameof(CreateAsync), email.Id);
    }
}
