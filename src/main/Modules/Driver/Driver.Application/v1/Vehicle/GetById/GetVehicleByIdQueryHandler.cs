using JacksonVeroneze.NET.Result;
using TaxiService.Common.SharedKernel.Interfaces;
using TaxiService.Modules.Driver.Application.v1.Vehicle.Common.Models;
using TaxiService.Modules.Driver.Domain.Core.Errors;
using IVehicleReadRepository =
    TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories.IVehicleReadRepository;

namespace TaxiService.Modules.Driver.Application.v1.Vehicle.GetById;

public sealed class GetVehicleByIdQueryHandler(
    IDataMapper mapper,
    IVehicleReadRepository vehicleReadRepository)
    : IRequestHandler<GetVehicleByIdQuery, Result<GetVehicleByIdQueryResponse>>
{
    public async Task<Result<GetVehicleByIdQueryResponse>> Handle(
        GetVehicleByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        VehicleDto? dto = await vehicleReadRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (dto is null)
        {
            return Result<GetVehicleByIdQueryResponse>
                .FromNotFound(DomainErrors.VehicleErrors.NotFound);
        }

        GetVehicleByIdQueryResponse response =
            mapper.Map<GetVehicleByIdQueryResponse>(dto);

        return Result<GetVehicleByIdQueryResponse>
            .WithSuccess(response);
    }
}
