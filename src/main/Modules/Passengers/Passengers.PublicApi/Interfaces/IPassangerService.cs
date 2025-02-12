using JacksonVeroneze.NET.Result;
using TaxiService.Modules.Passengers.Application.v1.Models.Passanger;

namespace TaxiService.Modules.Passengers.PublicApi.Interfaces;

public interface IPassangerService
{
    Task<Result<PassangerResponse>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}