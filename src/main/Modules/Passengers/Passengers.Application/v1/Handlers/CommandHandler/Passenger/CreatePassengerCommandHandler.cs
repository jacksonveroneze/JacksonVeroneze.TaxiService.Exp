using System.Text.Json;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories.Outbox;
using TaxiService.Modules.Passengers.Application.v1.Commands.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Interfaces;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;
using EmailEntity = TaxiService.Modules.Passengers.Domain.Entities.EmailEntity;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.Passenger;

public sealed class CreatePassengerCommandHandler(
    ILogger<CreatePassengerCommandHandler> logger,
    IDataMapper mapper,
    IPassengerRepository passengerWriteRepository,
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

        Result resultVerifyEmail = await ExistEmailAsync(
            request.Email!, cancellationToken);

        if (resultVerifyEmail.IsFailure)
        {
            return Result<CreatePassengerCommandResponse>
                .WithError(resultVerifyEmail.FirstError!);
        }

        Result<PassengerEntity> passengerEntity = FactoryEntity(request);

        if (passengerEntity.IsFailure)
        {
            return Result<CreatePassengerCommandResponse>
                .FromInvalid(passengerEntity.Errors);
        }

        Result<EmailEntity> emailEntity = FactoryEmailEntity(
            request, passengerEntity.Value!);

        if (emailEntity.IsFailure)
        {
            return Result<CreatePassengerCommandResponse>
                .FromInvalid(passengerEntity.Errors);
        }

        await CreateAsync(passengerEntity.Value!,
            emailEntity.Value!, cancellationToken);

        CreatePassengerCommandResponse response =
            mapper.Map<CreatePassengerCommandResponse>(passengerEntity.Value);

        return Result<CreatePassengerCommandResponse>.WithSuccess(response);
    }

    private async Task<Result> ExistEmailAsync(
        string email,
        CancellationToken cancellationToken)
    {
        bool existsEmail = await emailReadRepository
            .ExistsAsync(email, cancellationToken);

        if (!existsEmail)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.PassengerError.DuplicateEmail;

        logger.LogAlreadyExists(nameof(CreatePassengerCommandHandler),
            nameof(ExistEmailAsync), email, error);

        return Result.FromInvalid(error);
    }

    private Result<PassengerEntity> FactoryEntity(
        CreatePassengerCommand request)
    {
        Result<PassengerEntity> entity = PassengerEntity.Create(
            request.FullName, request.Birthday!.Value,
            request.Gender!.Value, request.Document);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(CreatePassengerCommandHandler),
            nameof(FactoryEntity), entity.Errors.Count);

        return Result<PassengerEntity>
            .FromInvalid(entity.Errors);
    }

    private Result<EmailEntity> FactoryEmailEntity(
        CreatePassengerCommand request, PassengerEntity passenger)
    {
        Result<EmailEntity> entity = EmailEntity
            .Create(passenger.Id, request.Email);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(CreatePassengerCommandHandler),
            nameof(Handle), entity.Errors.Count);

        return Result<EmailEntity>
            .FromInvalid(entity.Errors);
    }

    private static OutboxMessageEntity FactoryOutboxMessageEntity(
        PassengerEntity passenger, EmailEntity email)
    {
        var content = new { PassengerId = passenger.Id, Email = email.Email.Value };

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

        try
        {
            await passengerWriteRepository.CreateAsync(
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
        catch (Exception ex)
        {
            logger.LogGenericError(
                nameof(CreatePassengerCommandHandler),
                nameof(CreateAsync), ex.Message);

            throw;
        }
    }
}
