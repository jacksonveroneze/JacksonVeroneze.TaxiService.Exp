using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Drivers.Application.Common;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.v1.Driver.Common.Models;
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

        DriverDto? dto = await driverReadRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (dto is null)
        {
            Error error = DomainErrors.DriverErrors.NotFound;

            logger.LogNotFound(
                ModuleContext.ContextName,
                nameof(GetDriverByIdQueryHandler),
                nameof(Handle), request.Id, error);

            return Result<GetDriverByIdQueryResponse>
                .FromNotFound(error);
        }

        GetDriverByIdQueryResponse response =
            mapper.Map<GetDriverByIdQueryResponse>(dto);

        logger.LogGetById(ModuleContext.ContextName,
            nameof(GetDriverByIdQueryHandler), nameof(Handle), request.Id);

        return Result<GetDriverByIdQueryResponse>
            .WithSuccess(response);
    }
}
