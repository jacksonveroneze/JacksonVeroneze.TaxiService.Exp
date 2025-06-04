using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Passengers.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Repositories;

namespace TaxiService.Modules.Passengers.Application.v1.Email.Create;

public sealed class CreateEmailCommandHandler(
    ILogger<CreateEmailCommandHandler> logger,
    IDataMapper mapper,
    IPassengerRepository passengerRepository,
    IEmailReadRepository emailReadRepository,
    IEmailRepository emailRepository)
    : IRequestHandler<CreateEmailCommand, Result<CreateEmailCommandResponse>>
{
    public async Task<Result<CreateEmailCommandResponse>> Handle(
        CreateEmailCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Domain.Aggregates.Passenger? passenger = await passengerRepository
            .GetByIdAsync(request.PassengerId, cancellationToken);

        if (passenger is null)
        {
            return Result<CreateEmailCommandResponse>.FromNotFound(
                DomainErrors.PassengerError.NotFound);
        }

        bool existsEmail = await emailReadRepository
            .ExistsAsync(request.Body!.Email!, cancellationToken);

        if (existsEmail)
        {
            return Result<CreateEmailCommandResponse>.FromConflict(
                DomainErrors.PassengerError.DuplicateEmail);
        }

        Result<Domain.Entities.Email> entity = FactoryEntity(
            request, passenger);

        if (entity.IsFailure)
        {
            return Result<CreateEmailCommandResponse>
                .FromInvalid(entity.Errors);
        }

        await emailRepository.CreateAsync(
            entity.Value!, cancellationToken);

        logger.LogProcessed(
            nameof(CreateEmailCommandHandler),
            nameof(Handle), entity.Value!.Id);

        CreateEmailCommandResponse response =
            mapper.Map<CreateEmailCommandResponse>(entity.Value);

        return Result<CreateEmailCommandResponse>.WithSuccess(response);
    }

    private Result<Domain.Entities.Email> FactoryEntity(
        CreateEmailCommand request, Domain.Aggregates.Passenger passenger)
    {
        Result<Domain.Entities.Email> entity = Domain.Entities.Email
            .Create(passenger.Id, request.Body!.Email);

        if (entity.IsFailure)
        {
            logger.LogGenericError(
                nameof(CreateEmailCommandHandler),
                nameof(FactoryEntity), entity.Errors.Count);
        }

        return entity;
    }
}
