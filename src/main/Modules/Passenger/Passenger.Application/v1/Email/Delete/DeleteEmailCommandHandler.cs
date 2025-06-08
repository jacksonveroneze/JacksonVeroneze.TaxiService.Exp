using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passenger.Application.Common.Interfaces.Repositories;
using TaxiService.Modules.Passenger.Application.v1.Passenger.Activate;
using TaxiService.Modules.Passenger.Domain;
using TaxiService.Modules.Passenger.Domain.Core.Errors;
using TaxiService.Modules.Passenger.Domain.Repositories;

namespace TaxiService.Modules.Passenger.Application.v1.Email.Delete;

public sealed class DeleteEmailCommandHandler(
    ILogger<DeleteEmailCommandHandler> logger,
    IPassengerReadRepository passengerRepository,
    IEmailReadRepository emailReadRepository,
    IEmailRepository emailRepository)
    : IRequestHandler<DeleteEmailCommand, Result>
{
    public async Task<Result> Handle(
        DeleteEmailCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        bool existsPassenger = await passengerRepository
            .ExistsByIdAsync(request.PassengerId, cancellationToken);

        if (!existsPassenger)
        {
            return Result<DeleteEmailCommandHandler>.FromNotFound(
                DomainErrors.PassengerError.NotFound);
        }

        Domain.Entities.Email? email = await emailReadRepository
            .GetByIdAsync(request.EmailId, cancellationToken);

        if (email is null)
        {
            return Result<Domain.Entities.Email>.FromNotFound(
                DomainErrors.EmailError.NotFound);
        }

        await emailRepository.DeleteAsync(
            email, cancellationToken);

        logger.LogProcessed(
            ModuleConstants.ContextName,
            nameof(ActivatePassengerCommandHandler),
            nameof(Handle), email.Id);

        return Result.WithSuccess();
    }
}
