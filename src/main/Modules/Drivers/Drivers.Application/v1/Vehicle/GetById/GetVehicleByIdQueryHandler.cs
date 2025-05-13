using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Drivers.Application.Common;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Services;
using TaxiService.Modules.Drivers.Application.v1.Vehicle.Common.Models;

namespace TaxiService.Modules.Drivers.Application.v1.Vehicle.GetById;

public sealed class GetVehicleByIdQueryHandler(
    ILogger<GetVehicleByIdQueryHandler> logger,
    IDataMapper mapper,
    IVehicleService vehicleService)
    : IRequestHandler<GetVehicleByIdQuery, Result<GetVehicleByIdQueryResponse>>
{
    public async Task<Result<GetVehicleByIdQueryResponse>> Handle(
        GetVehicleByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<VehicleDto> resultEntity = await vehicleService
            .GetDtoByIdAsync(request.Id, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return Result<GetVehicleByIdQueryResponse>
                .FromNotFound(resultEntity.Error!);
        }

        GetVehicleByIdQueryResponse response =
            mapper.Map<GetVehicleByIdQueryResponse>(resultEntity.Value);

        logger.LogGetById(ModuleContext.ContextName,
            nameof(GetVehicleByIdQueryHandler), 
            nameof(Handle), request.Id);

        return Result<GetVehicleByIdQueryResponse>
            .WithSuccess(response);
    }
}
