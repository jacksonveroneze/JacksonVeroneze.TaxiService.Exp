using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Repositories.Ride;
using TaxiService.Modules.Rides.Application.v1.Interfaces.Services;
using TaxiService.Modules.Rides.Application.v1.Models.Ride;
using TaxiService.Modules.Rides.Application.v1.Queries.Ride;
using TaxiService.Modules.Rides.Domain.Core.Errors;
using TaxiService.Modules.Rides.Domain.Entities;

namespace TaxiService.Modules.Rides.Application.v1.Handlers.QueryHandler.Ride;

public sealed class GetByIdQueryHandler(
    ILogger<GetByIdQueryHandler> logger,
    IDataMapper mapper,
    IRideReadRepository rideReadRepository,
    IRideExpandsService rideExpandsService)
    : IRequestHandler<GetByIdQuery, Result<GetByIdQueryResponse>>
{
    public async Task<Result<GetByIdQueryResponse>> Handle(
        GetByIdQuery request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        RideDto? dto = await rideReadRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (dto is null)
        {
            Error error = DomainErrors.RideError.NotFound;

            logger.LogNotFound(
                nameof(GetByIdQueryHandler),
                nameof(Handle), request.Id, error);

            return Result<GetByIdQueryResponse>
                .FromNotFound(error);
        }

        GetByIdQueryResponse response =
            mapper.Map<GetByIdQueryResponse>(dto);

        await rideExpandsService.ApplyAsync(
            response.Data!, request.Expand, cancellationToken);
        
        logger.LogGetById(nameof(GetByIdQueryHandler),
            nameof(Handle), request.Id);

        return Result<GetByIdQueryResponse>
            .WithSuccess(response);
    }
}
