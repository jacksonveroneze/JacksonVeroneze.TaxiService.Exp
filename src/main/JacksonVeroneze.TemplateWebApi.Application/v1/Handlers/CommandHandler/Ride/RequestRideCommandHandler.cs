using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Ride;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.Ride;

public sealed class RequestRideCommandHandler(
    ILogger<RequestRideCommandHandler> logger,
    IMapper mapper,
    IUserReadRepository userReadRepository,
    IRideReadRepository readRepository,
    IRideWriteRepository writeRepository)
    : IRequestHandler<RequestRideCommand, IResult<RequestRideCommandResponse>>
{
    public async Task<IResult<RequestRideCommandResponse>> Handle(
        RequestRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserEntity? user = await userReadRepository
            .GetByIdAsync(request.UserId, cancellationToken);

        Task<bool> existsRideByUserTask = readRepository
            .ExistsByUserAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result<RequestRideCommandResponse>.Invalid(
                DomainErrors.User.NotFound);
        }

        bool existsRideByUser = await existsRideByUserTask;

        if (existsRideByUser)
        {
            logger.LogAlreadyExists(nameof(RequestRideCommandHandler),
                nameof(Handle), user.Id, DomainErrors.Ride.AlreadyByUser);

            return Result<RequestRideCommandResponse>.Invalid(
                DomainErrors.Ride.AlreadyByUser);
        }

        IResult<CoordinateValueObject> from = CoordinateValueObject.Create(
            request.LatitudeFrom, request.LongitudeFrom);

        IResult<CoordinateValueObject> to = CoordinateValueObject.Create(
            request.LatitudeTo, request.LongitudeTo);

        IResult resultValidate = Result.FailuresOrSuccess(from, to);

        if (resultValidate.IsFailure)
        {
            logger.LogGenericError(nameof(RequestRideCommandHandler),
                nameof(Handle), resultValidate.Errors!.Count());

            return Result<RequestRideCommandResponse>
                .Invalid(resultValidate.Errors!);
        }

        RideEntity entity = new(user, from.Value, to.Value);

        await writeRepository.CreateAsync(
            entity, cancellationToken);

        RequestRideCommandResponse response =
            mapper.Map<RequestRideCommandResponse>(entity);

        logger.LogCreated(nameof(RequestRideCommandHandler),
            nameof(Handle), entity.Id);

        return Result<RequestRideCommandResponse>.Success(response);
    }
}
