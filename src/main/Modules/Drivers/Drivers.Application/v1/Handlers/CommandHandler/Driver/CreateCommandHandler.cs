using System.Diagnostics;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Drivers.Application.v1.Commands.Driver;
using TaxiService.Modules.Drivers.Application.v1.Interfaces;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.v1.Models.Driver;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Handlers.CommandHandler.Driver;

public sealed class CreateCommandHandler(
    ILogger<CreateCommandHandler> logger,
    IDataMapper mapper,
    IDriverReadRepository driverReadRepository,
    IDriverRepository driverRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCommand, Result<CreateCommandResponse>>
{
    private static readonly ActivitySource ActivitySource =
        new("TaxiService");

    public async Task<Result<CreateCommandResponse>> Handle(
        CreateCommand request,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        ArgumentNullException.ThrowIfNull(request);

        Result resultVerifyEmailOrDocument = await ExistByEmailOrDocumentAsync(
            request.Email!, request.Document!, cancellationToken);

        if (resultVerifyEmailOrDocument.IsFailure)
        {
            return Result<CreateCommandResponse>
                .WithError(resultVerifyEmailOrDocument.Error!);
        }

        Result<DriverEntity> userEntity = FactoryEntity(request);

        if (userEntity.IsFailure)
        {
            return Result<CreateCommandResponse>
                .FromInvalid(userEntity.Errors!);
        }

        await CreateAsync(userEntity.Value!,
            cancellationToken);

        CreateCommandResponse response =
            mapper.Map<CreateCommandResponse>(userEntity.Value);

        return Result<CreateCommandResponse>.WithSuccess(response);
    }

    private async Task<Result> ExistByEmailOrDocumentAsync(
        string email, string document,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        bool existsEmail = await driverReadRepository
            .ExistsByEmailAndDocumentAsync(email, document, cancellationToken);

        if (!existsEmail)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.DriverErrors.DuplicateEmailOrDocument;

        logger.LogAlreadyExists(nameof(CreateCommandHandler),
            nameof(ExistByEmailOrDocumentAsync), email, error);

        return Result.FromInvalid(error);
    }

    private Result<DriverEntity> FactoryEntity(
        CreateCommand request)
    {
        using Activity? activity = ActivitySource.StartActivity();

        Result<DriverEntity> entity = DriverEntity.Create(
            request.FullName, request.Birthday!.Value,
            request.Email!, request.Phone!,
            request.Document, request.Gender!.Value);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(CreateCommandHandler),
            nameof(FactoryEntity), entity.Errors!.Count());

        return Result<DriverEntity>
            .FromInvalid(entity.Errors!);
    }

    private async Task CreateAsync(
        DriverEntity driverEntity,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        try
        {
            await driverRepository.CreateAsync(
                driverEntity, cancellationToken);

            await unitOfWork.CommitAsync(
                cancellationToken);

            logger.LogCreated(nameof(CreateCommandHandler),
                nameof(CreateAsync), driverEntity.Id);
        }
        catch (Exception ex)
        {
            logger.LogGenericError(
                nameof(CreateCommandHandler),
                nameof(CreateAsync), ex.Message);

            throw;
        }
    }
}