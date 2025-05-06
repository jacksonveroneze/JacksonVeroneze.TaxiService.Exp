using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Vehicle;
using TaxiService.Modules.Drivers.Domain;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Vehicle.GetById;

public sealed class GetVehicleByIdQueryHandler(
    ILogger<GetVehicleByIdQueryHandler> logger,
    IDataMapper mapper,
    IVehicleReadRepository driverReadRepository)
    : IRequestHandler<GetVehicleByIdQuery, Result<GetVehicleByIdQueryResponse>>
{
    public async Task<Result<GetVehicleByIdQueryResponse>> Handle(
        GetVehicleByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<VehicleEntity> resultEntity = await GetByIdAsync(
            request.Id, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return Result<GetVehicleByIdQueryResponse>
                .FromNotFound(resultEntity.Error!);
        }

        GetVehicleByIdQueryResponse response =
            mapper.Map<GetVehicleByIdQueryResponse>(resultEntity.Value);

        logger.LogGetById(ContextInfo.ContextName,
            nameof(GetVehicleByIdQueryHandler), nameof(Handle), request.Id);

        return Result<GetVehicleByIdQueryResponse>
            .WithSuccess(response);
    }

    private async Task<Result<VehicleEntity>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        VehicleEntity? vehicle = await driverReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (vehicle is not null)
        {
            return Result<VehicleEntity>.WithSuccess(vehicle);
        }

        Error error = DomainErrors.VehicleErrors.NotFound;

        logger.LogNotFound(
            nameof(GetVehicleByIdQueryHandler),
            nameof(GetByIdAsync), id, error);

        return Result<VehicleEntity>.FromNotFound(error);
    }
}
