using System.Diagnostics;
using System.Text.Json;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;
using TaxiService.Modules.Passengers.Application.v1.Commands.Passanger;
using TaxiService.Modules.Passengers.Application.v1.Interfaces;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passanger;
using TaxiService.Modules.Passengers.Application.v1.Models.Passanger;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;
using EmailEntity = TaxiService.Modules.Passengers.Domain.Entities.EmailEntity;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.Passanger;

public sealed class CreateCommandHandler(
    ILogger<CreateCommandHandler> logger,
    IDataMapper mapper,
    IPassangerWriteRepository passangerWriteRepository,
    IEmailReadRepository emailReadRepository,
    IEmailWriteRepository emailWriteRepository,
    IOutboxWriteRepository outboxWriteRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCommand, Result<CreateCommandResponse>>
{
    private static readonly ActivitySource ActivitySource =
        new("TaxiService");

    public async Task<Result<CreateCommandResponse>> Handle(
        CreateCommand request,
        CancellationToken cancellationToken)
    {
        using var activity = OpenTelemetryExtensions
            .ActivitySource.StartActivity()!;

        ArgumentNullException.ThrowIfNull(request);

        Result resultVerifyEmail = await ExistEmailAsync(
            request.Email!, cancellationToken);

        if (resultVerifyEmail.IsFailure)
        {
            return Result<CreateCommandResponse>
                .WithError(resultVerifyEmail.Error!);
        }

        Result<PassangerEntity> passangerEntity = FactoryEntity(request);

        if (passangerEntity.IsFailure)
        {
            return Result<CreateCommandResponse>
                .FromInvalid(passangerEntity.Errors!);
        }

        Result<EmailEntity> emailEntity = FactoryEmailEntity(
            request, passangerEntity.Value!);

        if (emailEntity.IsFailure)
        {
            return Result<CreateCommandResponse>
                .FromInvalid(passangerEntity.Errors!);
        }

        await CreateAsync(passangerEntity.Value!,
            emailEntity.Value!, cancellationToken);

        CreateCommandResponse response =
            mapper.Map<CreateCommandResponse>(passangerEntity.Value);

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

        Error error = DomainErrors.PassangerError.DuplicateEmail;

        logger.LogAlreadyExists(nameof(CreateCommandHandler),
            nameof(ExistEmailAsync), email, error);

        return Result.FromInvalid(error);
    }

    private Result<PassangerEntity> FactoryEntity(
        CreateCommand request)
    {
        using Activity? activity = ActivitySource.StartActivity();

        Result<PassangerEntity> entity = PassangerEntity.Create(
            request.FullName, request.Birthday!.Value,
            request.Gender!.Value, request.Document);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(CreateCommandHandler),
            nameof(FactoryEntity), entity.Errors!.Count());

        return Result<PassangerEntity>
            .FromInvalid(entity.Errors!);
    }

    private Result<EmailEntity> FactoryEmailEntity(
        CreateCommand request, PassangerEntity passanger)
    {
        using Activity? activity = ActivitySource.StartActivity();

        Result<EmailEntity> entity = EmailEntity
            .Create(passanger.Id, request.Email);

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

    private static OutboxMessageEntity FactoryOutboxMessageEntity(
        PassangerEntity passanger, EmailEntity email)
    {
        using Activity? activity = ActivitySource.StartActivity();

        var content = new { PassangerId = passanger.Id, Email = email.Email.Value };

        string jsonContent = JsonSerializer.Serialize(content);

        OutboxMessageEntity entity = OutboxMessageEntity
            .Create(nameof(PassangerEntity), jsonContent);

        return entity;
    }

    private async Task CreateAsync(
        PassangerEntity passangerEntity,
        EmailEntity emailEntity,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        OutboxMessageEntity message =
            FactoryOutboxMessageEntity(passangerEntity, emailEntity);

        try
        {
            await passangerWriteRepository.CreateAsync(
                passangerEntity, cancellationToken);

            await emailWriteRepository.CreateAsync(
                emailEntity, cancellationToken);

            await outboxWriteRepository
                .CreateAsync(message, cancellationToken);

            await unitOfWork.CommitAsync(
                cancellationToken);

            logger.LogCreated(nameof(CreateCommandHandler),
                nameof(CreateAsync), passangerEntity.Id);
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

public static class OpenTelemetryExtensions
{
    public static string ServiceName { get; }
    public static string ServiceVersion { get; }
    public static ActivitySource ActivitySource { get; }

    static OpenTelemetryExtensions()
    {
        ServiceName = "APIContagemPostgreSQL";
        ServiceVersion = typeof(OpenTelemetryExtensions).Assembly.GetName().Version!.ToString();
        ActivitySource = new ActivitySource(ServiceName, ServiceVersion);
    }
}