using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Ride;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.Ride;

public sealed class RequestRideCommandHandler(
    ILogger<RequestRideCommandHandler> logger,
    IMapper mapper,
    IUserReadRepository userReadRepository,
    IRideReadRepository readRepository,
    IRideWriteRepository writeRepository)
    : IRequestHandler<RequestRideCommand, Result<RequestRideCommandResponse>>
{
    public async Task<Result<RequestRideCommandResponse>> Handle(
        RequestRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserEntity? user = await userReadRepository
            .GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result<RequestRideCommandResponse>.WithError(
                DomainErrors.User.NotFound);
        }

        bool existsRideByUser = await readRepository
            .ExistsActiveByUserIdAsync(request.UserId, cancellationToken);

        if (existsRideByUser)
        {
            logger.LogAlreadyExists(nameof(RequestRideCommandHandler),
                nameof(Handle), user.Id, DomainErrors.Ride.AlreadyByUser);

            return Result<RequestRideCommandResponse>.FromInvalid(
                DomainErrors.Ride.AlreadyByUser);
        }

        Result<RideEntity> entity = RideEntity.Create(user.Id,
            request.LatitudeFrom, request.LatitudeTo,
            request.LongitudeFrom, request.LongitudeTo);

        if (entity.IsFailure)
        {
            logger.LogGenericError(nameof(RequestRideCommandHandler),
                nameof(Handle), entity.Errors!.Count());

            return Result<RequestRideCommandResponse>
                .FromInvalid(entity.Errors!);
        }

        await writeRepository.CreateAsync(
            entity.Value!, cancellationToken);

        RequestRideCommandResponse response =
            mapper.Map<RequestRideCommandResponse>(entity.Value);

        logger.LogCreated(nameof(RequestRideCommandHandler),
            nameof(Handle), entity.Value!.Id);

        return Result<RequestRideCommandResponse>.WithSuccess(response);
    }
}
