using AutoMapper;
using JacksonVeroneze.NET.Result;
using Microsoft.Extensions.Logging;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passanger;
using TaxiService.Modules.Passengers.Application.v1.Models.Passanger;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;
using TaxiService.Modules.Passengers.PublicApi.Interfaces;

namespace TaxiService.Modules.Passengers.PublicApi.Services;

public class PassangerService(
    ILogger<PassangerService> logger,
    IMapper mapper,
    IPassangerReadRepository passangerReadRepository) : IPassangerService
{
    public async Task<Result<PassangerResponse>> GetByIdAsync(
        Guid id, CancellationToken cancellationToken)
    {
        PassangerEntity? entity = await passangerReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (entity is not null)
        {
            logger.LogGetById(
                nameof(PassangerService),
                nameof(GetByIdAsync), id);

            var response = mapper.Map<PassangerResponse>(entity);

            return Result<PassangerResponse>.WithSuccess(response);
        }

        Error error = DomainErrors.PassangerError.NotFound;

        logger.LogNotFound(
            nameof(PassangerService),
            nameof(GetByIdAsync), id, error);

        return Result<PassangerResponse>.FromNotFound(error);
    }
}