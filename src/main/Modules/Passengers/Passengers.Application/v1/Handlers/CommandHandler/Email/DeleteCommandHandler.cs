using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Commands.Email;
using TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.Email;

public sealed class DeleteCommandHandler(
    ILogger<DeleteCommandHandler> logger,
    IPassengerReadRepository passengerReadRepository,
    IEmailReadRepository emailReadRepository,
    IEmailWriteRepository emailWriteRepository)
    : IRequestHandler<DeleteCommand, Result>
{
    public async Task<Result> Handle(
        DeleteCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<PassengerEntity> resultPassenger = await GetByIdAsync(
            request.Id, cancellationToken);

        if (resultPassenger.IsFailure)
        {
            return resultPassenger;
        }

        Result<EmailEntity> resultEmail = await GetEmailByIdAsync(
            request.EmailId, cancellationToken);

        if (resultEmail.IsFailure)
        {
            return resultEmail;
        }

        await DeleteAsync(resultEmail.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private async Task<Result<PassengerEntity>> GetByIdAsync(
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

    private async Task<Result<EmailEntity>> GetEmailByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        EmailEntity? email = await emailReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (email is not null)
        {
            return Result<EmailEntity>.WithSuccess(email);
        }

        Error error = DomainErrors.EmailError.NotFound;

        logger.LogNotFound(
            nameof(DeleteCommandHandler),
            nameof(Handle), id, error);

        return Result<EmailEntity>.FromNotFound(error);
    }

    private async Task DeleteAsync(
        EmailEntity email,
        CancellationToken cancellationToken)
    {
        await emailWriteRepository.DeleteAsync(
            email, cancellationToken);

        logger.LogProcessed(
            nameof(ActivateCommandHandler),
            nameof(DeleteAsync), email.Id);
    }
}
