using System.Diagnostics;

using JacksonVeroneze.NET.Result;

using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Drivers.Application.Common.Interfaces;
using TaxiService.Modules.Drivers.Application.Common.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Driver.Create;

public sealed class CreateDriverCommandHandler(
    ILogger<CreateDriverCommandHandler> logger,
    IDataMapper mapper,
    IDriverReadRepository driverReadRepository,
    IDriverRepository driverRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateDriverCommand, Result<CreateDriverCommandResponse>>
{
    private static readonly ActivitySource ActivitySource =
        new("TaxiService");

    public async Task<Result<CreateDriverCommandResponse>> Handle(
        CreateDriverCommand request,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        ArgumentNullException.ThrowIfNull(request);

        Result resultVerifyEmailOrDocument =
            await ExistByEmailOrDocumentAsync(
                request.Email!, cancellationToken);

        if (resultVerifyEmailOrDocument.IsFailure)
        {
            return Result<CreateDriverCommandResponse>
                .WithError(resultVerifyEmailOrDocument.Error!);
        }

        Result<DriverEntity> userEntity = FactoryEntity(request);

        if (userEntity.IsFailure)
        {
            return Result<CreateDriverCommandResponse>
                .FromInvalid(userEntity.Errors!);
        }

        await CreateAsync(userEntity.Value!,
            cancellationToken);

        CreateDriverCommandResponse response =
            mapper.Map<CreateDriverCommandResponse>(userEntity.Value);

        return Result<CreateDriverCommandResponse>.WithSuccess(response);
    }

    private async Task<Result> ExistByEmailOrDocumentAsync(
        string email,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        bool existsEmail = await driverReadRepository
            .ExistsByEmailAsync(email, cancellationToken);

        if (!existsEmail)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.DriverErrors.DuplicateEmailOrDocument;

        logger.LogAlreadyExists(nameof(CreateDriverCommandHandler),
            nameof(ExistByEmailOrDocumentAsync), email, error);

        return Result.FromInvalid(error);
    }

    private Result<DriverEntity> FactoryEntity(
        CreateDriverCommand request)
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
            nameof(CreateDriverCommandHandler),
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

            logger.LogCreated(nameof(CreateDriverCommandHandler),
                nameof(CreateAsync), driverEntity.Id);
        }
        catch (Exception ex)
        {
            logger.LogGenericError(
                nameof(CreateDriverCommandHandler),
                nameof(CreateAsync), ex.Message);

            throw;
        }
    }
}
