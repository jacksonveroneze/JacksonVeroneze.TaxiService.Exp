using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Services.Ride;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Services.Ride;

public sealed class GetRideService(
    IRideReadRepository readRepository)
    : IGetRideService
{
    public async Task<Result<RideEntity>> TryGetRideAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        RideEntity? entity = await readRepository
            .GetByIdAsync(id, cancellationToken);

        return entity is not null
            ? Result<RideEntity>.WithSuccess(entity)
            : Result<RideEntity>.FromNotFound(DomainErrors.Ride.NotFound);
    }
}
