using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Services.Ride;

public interface IGetRideService
{
    Task<IResult<RideEntity>> TryGetRideAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
