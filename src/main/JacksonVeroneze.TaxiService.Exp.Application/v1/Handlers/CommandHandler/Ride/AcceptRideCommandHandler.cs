using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.Ride;

public sealed class AcceptRideCommandHandler(
    ILogger<AcceptRideCommandHandler> logger,
    IUserReadRepository userReadRepository,
    IRideReadRepository readRepository,
    IRideWriteRepository writeRepository)
    : IRequestHandler<AcceptRideCommand, Result<VoidResponse>>
{
    public async Task<Result<VoidResponse>> Handle(
        AcceptRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        RideEntity? ride = await readRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (ride is null)
        {
            return Result<VoidResponse>
                .FromNotFound(DomainErrors.RideError.NotFound);
        }

        UserEntity? user = await userReadRepository
            .GetByIdAsync(request.Body!.DriverId,
                cancellationToken);

        if (user is null)
        {
            return Result<VoidResponse>
                .WithError(DomainErrors.UserError.NotFound);
        }

        Result result = ride.Accept(user.Id);

        if (result.IsFailure)
        {
            logger.LogGenericError(nameof(AcceptRideCommandHandler),
                nameof(Handle), ride.Id, result.Error!);

            return Result<VoidResponse>.WithError(result.Error!);
        }

        await writeRepository.UpdateAsync(ride, cancellationToken);

        logger.LogProcessed(nameof(AcceptRideCommandHandler),
            nameof(Handle), ride.Id);

        return Result<VoidResponse>.WithSuccess();
    }
}