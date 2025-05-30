using System.Text.Json;

using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Passengers.Application.Common.Interfaces;
using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Outbox;
using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;

using EmailEntity = TaxiService.Modules.Passengers.Domain.Entities.EmailEntity;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.Create;

public sealed class CreatePassengerCommandHandler(
    ILogger<CreatePassengerCommandHandler> logger,
    IDataMapper mapper,
    IPassengerRepository passengerRepository,
    IEmailReadRepository emailReadRepository,
    IEmailWriteRepository emailWriteRepository,
    IOutboxWriteRepository outboxWriteRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreatePassengerCommand, Result<CreatePassengerCommandResponse>>
{
    public async Task<Result<CreatePassengerCommandResponse>> Handle(
        CreatePassengerCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        bool existsEmail = await emailReadRepository
            .ExistsAsync(request.Email!, cancellationToken);

        if (existsEmail)
        {
            Error error = DomainErrors.PassengerError.DuplicateEmail;

            logger.LogAlreadyExists(nameof(CreatePassengerCommandHandler),
                nameof(Handle), request.Email!, error);

            return Result<CreatePassengerCommandResponse>.FromConflict(error);
        }

        Result<PassengerEntity> passengerEntity = 
            FactoryEntity(request);

        Result<EmailEntity> emailEntity = 
            FactoryEmailEntity(request, passengerEntity.Value!);

        if (passengerEntity.IsFailure || emailEntity.IsFailure)
        {
            IEnumerable<Error> errors = passengerEntity.Errors
                .Concat(emailEntity.Errors);

            return Result<CreatePassengerCommandResponse>
                .FromInvalid(errors);
        }

        await CreateAsync(passengerEntity.Value!,
            emailEntity.Value!, cancellationToken);

        CreatePassengerCommandResponse response =
            mapper.Map<CreatePassengerCommandResponse>(
                passengerEntity.Value);

        return Result<CreatePassengerCommandResponse>.WithSuccess(response);
    }

    private Result<PassengerEntity> FactoryEntity(
        CreatePassengerCommand request)
    {
        Result<PassengerEntity> entity = PassengerEntity.Create(
            request.FullName, request.Birthday!.Value,
            request.Gender!.Value, request.Document);

        if (entity.IsFailure)
        {
            logger.LogGenericError(
                nameof(CreatePassengerCommandHandler),
                nameof(FactoryEntity), entity.Errors.Count);
        }

        return entity;
    }

    private Result<EmailEntity> FactoryEmailEntity(
        CreatePassengerCommand request, PassengerEntity passenger)
    {
        Result<EmailEntity> entity = EmailEntity
            .Create(passenger.Id, request.Email);

        if (entity.IsFailure)
        {
            logger.LogGenericError(
                nameof(CreatePassengerCommandHandler),
                nameof(Handle), entity.Errors.Count);
        }

        return entity;
    }

    private static OutboxMessageEntity FactoryOutboxMessageEntity(
        PassengerEntity passenger, EmailEntity email)
    {
        var content = new
        {
            PassengerId = passenger.Id,
            Email = email.Email.Value,
        };

        string jsonContent = JsonSerializer.Serialize(content);

        OutboxMessageEntity entity = OutboxMessageEntity
            .Create(nameof(PassengerEntity), jsonContent);

        return entity;
    }

    private async Task CreateAsync(
        PassengerEntity passengerEntity,
        EmailEntity emailEntity,
        CancellationToken cancellationToken)
    {
        OutboxMessageEntity message =
            FactoryOutboxMessageEntity(passengerEntity, emailEntity);

        await passengerRepository.CreateAsync(
            passengerEntity, cancellationToken);

        await emailWriteRepository.CreateAsync(
            emailEntity, cancellationToken);

        await outboxWriteRepository
            .CreateAsync(message, cancellationToken);

        await unitOfWork.CommitAsync(
            cancellationToken);

        logger.LogCreated(nameof(CreatePassengerCommandHandler),
            nameof(CreateAsync), passengerEntity.Id);
    }
}
