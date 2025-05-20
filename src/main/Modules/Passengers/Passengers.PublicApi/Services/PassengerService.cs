using AutoMapper;
using JacksonVeroneze.NET.Result;
using Microsoft.Extensions.Logging;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passenger;
using TaxiService.Modules.Passengers.Application.v1.Models.Passenger;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.PublicApi.Interfaces;

namespace TaxiService.Modules.Passengers.PublicApi.Services;

public class PassengerService(
    ILogger<PassengerService> logger,
    IMapper mapper,
    IPassengerReadRepository passengerReadRepository) : IPassengerService
{
    public async Task<Result<PassengerResponse>> GetByIdAsync(
        Guid id, CancellationToken cancellationToken)
    {
        PassengerEntity? entity = await passengerReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            logger.LogGetById(
                nameof(PassengerService),
                nameof(GetByIdAsync), id);

            var response = mapper.Map<PassengerResponse>(entity);

            return Result<PassengerResponse>.WithSuccess(response);
        }

        Error error = DomainErrors.PassengerError.NotFound;

        logger.LogNotFound(
            nameof(PassengerService),
            nameof(GetByIdAsync), id, error);

        return Result<PassengerResponse>.FromNotFound(error);
    }
}
