using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Drivers.Domain;
using TaxiService.Modules.Drivers.Domain.Core.Errors;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.GetById;

public sealed class GetByIdQueryHandler(
    ILogger<GetByIdQueryHandler> logger,
    IDataMapper mapper,
    IDriverReadRepository driverReadRepository)
    : IRequestHandler<GetByIdQuery, Result<GetByIdQueryResponse>>
{
    public async Task<Result<GetByIdQueryResponse>> Handle(
        GetByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<DriverDto> resultEntity = await GetByIdAsync(
            request.Id, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return Result<GetByIdQueryResponse>
                .FromNotFound(resultEntity.Error!);
        }

        GetByIdQueryResponse response =
            mapper.Map<GetByIdQueryResponse>(resultEntity.Value);

        logger.LogGetById(ContextInfo.ContextName,
            nameof(GetByIdQueryHandler), nameof(Handle), request.Id);

        return Result<GetByIdQueryResponse>
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
            nameof(GetByIdQueryHandler),
            nameof(GetByIdAsync), id, error);

        return Result<DriverDto>.FromNotFound(error);
    }
}
