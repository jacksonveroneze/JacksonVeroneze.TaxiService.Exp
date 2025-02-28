using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.v1.Commands.DriverAvailability;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.DriverAvailability;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Services;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;
using TaxiService.Modules.Drivers.Domain.Enums;

namespace TaxiService.Modules.Drivers.Application.v1.Handlers.CommandHandler.DriverAvailability;

public sealed class UpdateAvailabilityHandler(
    ILogger<UpdateAvailabilityHandler> logger,
    IDriverService driverService,
    IDriverAvailabilityReadRepository driverAvailabilityReadRepository,
    IDriverAvailabilityWriteRepository driverAvailabilityWriteRepository)
    : IRequestHandler<UpdateAvailabilityCommand, Result>
{
    public async Task<Result> Handle(
        UpdateAvailabilityCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result validationResult = await ValidateBussinesRulesAsync(
            request, cancellationToken);

        if (validationResult.IsFailure)
        {
            return validationResult;
        }
        
        Result<DriverAvailabilityEntity> driverAvailability = 
            FactoryEntity(request);

        if (driverAvailability.IsFailure)
        {
            return Result<UpdateAvailabilityHandler>
                .FromInvalid(driverAvailability.Errors!);
        }

        await CreateAsync(driverAvailability.Value!, cancellationToken);

        return Result.WithSuccess();
    }

    private async Task<Result> ValidateBussinesRulesAsync(
        UpdateAvailabilityCommand request,
        CancellationToken cancellationToken)
    {
        Result<DriverEntity> resultEntity =
            await driverService.GetByIdAsync(
                request.Id, cancellationToken);

        if (resultEntity.IsFailure)
        {
            return resultEntity;
        }

        var resultValidation = ValidateDriverStatus(resultEntity.Value!);
        
        if (resultValidation.IsFailure)
        {
            return resultValidation;
        }
        
        DriverAvailabilityEntity? lastAvailability =
            await driverAvailabilityReadRepository
                .GetLastByDriverIdAsync(request.Id, cancellationToken);

        if (lastAvailability is not null)
        {
            // Validar regras aqui.
        }
        
        return Result.WithSuccess();
    }
    
    private Result ValidateDriverStatus(
        DriverEntity entity)
    {
        if (entity.IsActive)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.DriverErrors.NotActivated;
            
        logger.LogGenericValidationError(
            nameof(UpdateAvailabilityHandler),
            nameof(ValidateDriverStatus), error);
            
        return Result<DriverEntity>.WithError(error);
    }
    
    private Result<DriverAvailabilityEntity> FactoryEntity(
        UpdateAvailabilityCommand request)
    {
        Result<DriverAvailabilityEntity> entity =
            DriverAvailabilityEntity.Create(
                request.Id, request.Status);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(UpdateAvailabilityHandler),
            nameof(FactoryEntity), entity.Errors!.Count());

        return Result<DriverAvailabilityEntity>
            .FromInvalid(entity.Errors!);
    }
    
    private async Task CreateAsync(
        DriverAvailabilityEntity entity,
        CancellationToken cancellationToken)
    {
        await driverAvailabilityWriteRepository.CreateAsync(
            entity, cancellationToken);

        logger.LogCreated(nameof(UpdateAvailabilityHandler),
            nameof(CreateAsync), entity.Id);
    }
}