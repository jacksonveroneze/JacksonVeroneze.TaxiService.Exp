using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Repositories.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Services.Ride;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Services.Ride;

public sealed class GetRideService(
    IRideReadRepository readRepository)
    : IGetRideService
{
    public async Task<IResult<RideEntity>> TryGetRideAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        RideEntity? entity = await readRepository
            .GetByIdAsync(id, cancellationToken);

        return entity is not null
            ? Result<RideEntity>.Success(entity)
            : Result<RideEntity>.NotFound(DomainErrors.Ride.NotFound);
    }
}
