using JacksonVeroneze.NET.Result;
using TaxiService.Common.SharedKernel.Interfaces;
using TaxiService.Modules.Driver.Application.v1.Driver.Common.Models;
using TaxiService.Modules.Driver.Domain.Core.Errors;
using IDriverReadRepository =
    TaxiService.Modules.Driver.Application.Common.Interfaces.Repositories.IDriverReadRepository;

namespace TaxiService.Modules.Driver.Application.v1.Driver.GetById;

public sealed class GetDriverByIdQueryHandler(
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
            return Result<GetDriverByIdQueryResponse>
                .FromNotFound(DomainErrors.DriverErrors.NotFound);
        }

        GetDriverByIdQueryResponse response =
            mapper.Map<GetDriverByIdQueryResponse>(dto);

        return Result<GetDriverByIdQueryResponse>
            .WithSuccess(response);
    }
}
