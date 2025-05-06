using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Drivers.Application.Common;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Drivers.Domain;
using TaxiService.Modules.Drivers.Domain.Core.Errors;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.GetById;

public sealed class GetDriverByIdQueryHandler(
    ILogger<GetDriverByIdQueryHandler> logger,
    IDataMapper mapper,
    IDriverReadRepository driverReadRepository)
    : IRequestHandler<GetDriverByIdQuery, Result<GetDriverByIdQueryResponse>>
{
    public async Task<Result<GetDriverByIdQueryResponse>> Handle(
        GetDriverByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<DriverDto> resultEntity = await GetByIdAsync(
            request.Id, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return Result<GetDriverByIdQueryResponse>
                .FromNotFound(resultEntity.Error!);
        }

        GetDriverByIdQueryResponse response =
            mapper.Map<GetDriverByIdQueryResponse>(resultEntity.Value);

        logger.LogGetById(ModuleContext.ContextName,
            nameof(GetDriverByIdQueryHandler), nameof(Handle), request.Id);

        return Result<GetDriverByIdQueryResponse>
            .WithSuccess(response);
    }

    private async Task<Result<DriverDto>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        DriverDto? driver = await driverReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (driver is not null)
        {
            return Result<DriverDto>.WithSuccess(driver);
        }

        Error error = DomainErrors.DriverErrors.NotFound;

        logger.LogNotFound(
            ModuleContext.ContextName,
            nameof(GetDriverByIdQueryHandler),
            nameof(GetByIdAsync), id, error);

        return Result<DriverDto>.FromNotFound(error);
    }
}
