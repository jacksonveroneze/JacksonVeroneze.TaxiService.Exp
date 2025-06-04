using System.Text.Json;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Common.BaseApplication.Interfaces.Messaging;
using TaxiService.Modules.Passengers.Application.Common.Interfaces;
using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.Domain.Repositories;

namespace TaxiService.Modules.Passengers.Application.v1.Passenger.Create;

public sealed class CreatePassengerCommandHandler(
    ILogger<CreatePassengerCommandHandler> logger,
    IDataMapper mapper,
    IPassengerRepository passengerRepository,
    IEmailReadRepository emailReadRepository,
    IEmailRepository emailRepository,
    IOutboxWriteRepository outboxWriteRepository,
    IUnitOfWork unitOfWork,
    IDomainEventDispatcher eventDispatcher)
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

        Result<Domain.Aggregates.Passenger> resultEntity =
            FactoryEntity(request);

        Result<Domain.Entities.Email> emailEntity =
            FactoryEmailEntity(request, resultEntity.Value!);

        if (resultEntity.IsFailure || emailEntity.IsFailure)
        {
            IEnumerable<Error> errors = resultEntity.Errors
                .Concat(emailEntity.Errors);

            return Result<CreatePassengerCommandResponse>
                .FromInvalid(errors);
        }

        await CreateAsync(resultEntity.Value!,
            emailEntity.Value!, cancellationToken);

        await eventDispatcher.DispatchAsync(resultEntity.Value!);

        CreatePassengerCommandResponse response =
            mapper.Map<CreatePassengerCommandResponse>(
                resultEntity.Value);

        return Result<CreatePassengerCommandResponse>.WithSuccess(response);
    }

    private Result<Domain.Aggregates.Passenger> FactoryEntity(
        CreatePassengerCommand request)
    {
        Result<Domain.Aggregates.Passenger> entity = Domain.Aggregates.Passenger.Create(
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

    private Result<Domain.Entities.Email> FactoryEmailEntity(
        CreatePassengerCommand request, Domain.Aggregates.Passenger passenger)
    {
        Result<Domain.Entities.Email> entity = Domain.Entities.Email
            .Create(passenger.Id, request.Email);

        if (entity.IsFailure)
        {
            logger.LogGenericError(
                nameof(CreatePassengerCommandHandler),
                nameof(Handle), entity.Errors.Count);
        }

        return entity;
    }

    private static OutboxMessage FactoryOutboxMessageEntity(
        Domain.Aggregates.Passenger passenger, Domain.Entities.Email email)
    {
        var content = new
        {
            PassengerId = passenger.Id,
            Email = email.Value.Value,
        };

        string jsonContent = JsonSerializer.Serialize(content);

        OutboxMessage entity = OutboxMessage
            .Create(nameof(Domain.Aggregates.Passenger), jsonContent);

        return entity;
    }

    private async Task CreateAsync(
        Domain.Aggregates.Passenger passenger,
        Domain.Entities.Email email,
        CancellationToken cancellationToken)
    {
        OutboxMessage message =
            FactoryOutboxMessageEntity(passenger, email);

        await passengerRepository.CreateAsync(
            passenger, cancellationToken);

        await emailRepository.CreateAsync(
            email, cancellationToken);

        await outboxWriteRepository
            .CreateAsync(message, cancellationToken);

        await unitOfWork.CommitAsync(
            cancellationToken);

        logger.LogCreated(nameof(CreatePassengerCommandHandler),
            nameof(CreateAsync), passenger.Id);
    }
}
