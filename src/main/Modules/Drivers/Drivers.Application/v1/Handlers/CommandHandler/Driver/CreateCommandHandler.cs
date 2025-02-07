using System.Diagnostics;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Drivers.Application.v1.Commands.Driver;
using TaxiService.Modules.Drivers.Application.v1.Interfaces;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Driver;
using TaxiService.Modules.Drivers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Drivers.Application.v1.Models.Driver;
using TaxiService.Modules.Drivers.Domain.Core.Errors;
using TaxiService.Modules.Drivers.Domain.Entities;

namespace TaxiService.Modules.Drivers.Application.v1.Handlers.CommandHandler.Driver;

public sealed class CreateCommandHandler(
    ILogger<CreateCommandHandler> logger,
    IMapper mapper,
    IDriverWriteRepository driverWriteRepository,
    IEmailReadRepository emailReadRepository,
    IEmailWriteRepository emailWriteRepository,
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

        Result resultVerifyEmail = await ExistEmailAsync(
            request.Email!, cancellationToken);

        if (resultVerifyEmail.IsFailure)
        {
            return Result<CreateCommandResponse>
                .WithError(resultVerifyEmail.Error!);
        }

        Result<DriverEntity> userEntity = FactoryEntity(request);

        if (userEntity.IsFailure)
        {
            return Result<CreateCommandResponse>
                .FromInvalid(userEntity.Errors!);
        }

        Result<EmailEntity> emailEntity = FactoryEmailEntity(
            request, userEntity.Value!);

        if (emailEntity.IsFailure)
        {
            return Result<CreateCommandResponse>
                .FromInvalid(userEntity.Errors!);
        }

        await CreateAsync(userEntity.Value!,
            emailEntity.Value!, cancellationToken);

        CreateCommandResponse response =
            mapper.Map<CreateCommandResponse>(userEntity.Value);

        return Result<CreateCommandResponse>.WithSuccess(response);
    }

    private async Task<Result> ExistEmailAsync(
        string email,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        bool existsEmail = await emailReadRepository
            .ExistsAsync(email, cancellationToken);

        if (!existsEmail)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.DriverError.DuplicateEmail;

        logger.LogAlreadyExists(nameof(CreateCommandHandler),
            nameof(ExistEmailAsync), email, error);

        return Result.FromInvalid(error);
    }

    private Result<DriverEntity> FactoryEntity(
        CreateCommand request)
    {
        using Activity? activity = ActivitySource.StartActivity();

        Result<DriverEntity> entity = DriverEntity.Create(
            request.FullName, request.Birthday!.Value,
            request.Gender!.Value, request.Document);

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

    private Result<EmailEntity> FactoryEmailEntity(
        CreateCommand request, DriverEntity driver)
    {
        using Activity? activity = ActivitySource.StartActivity();

        Result<EmailEntity> entity = EmailEntity
            .Create(driver.Id, request.Email);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(CreateCommandHandler),
            nameof(Handle), entity.Errors!.Count());

        return Result<EmailEntity>
            .FromInvalid(entity.Errors!);
    }

    private async Task CreateAsync(
        DriverEntity driverEntity,
        EmailEntity emailEntity,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();


        try
        {
            await driverWriteRepository.CreateAsync(
                driverEntity, cancellationToken);

            await emailWriteRepository.CreateAsync(
                emailEntity, cancellationToken);

            await unitOfWork.CommitAsync(
                cancellationToken);

            logger.LogCreated(nameof(CreateCommandHandler),
                nameof(CreateAsync), driverEntity!.Id);
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