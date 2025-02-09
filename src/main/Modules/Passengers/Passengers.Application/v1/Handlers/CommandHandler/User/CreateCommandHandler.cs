using System.Diagnostics;
using System.Text.Json;
using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Commands.User;
using TaxiService.Modules.Passengers.Application.v1.Interfaces;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.User;
using TaxiService.Modules.Passengers.Application.v1.Models.User;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;
using EmailEntity = TaxiService.Modules.Passengers.Domain.Entities.EmailEntity;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.User;

public sealed class CreateCommandHandler(
    ILogger<CreateCommandHandler> logger,
    IMapper mapper,
    IUserWriteRepository userWriteRepository,
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
        using Activity? activity = ActivitySource.StartActivity();

        ArgumentNullException.ThrowIfNull(request);

        Result resultVerifyEmail = await ExistEmailAsync(
            request.Email!, cancellationToken);

        if (resultVerifyEmail.IsFailure)
        {
            return Result<CreateCommandResponse>
                .WithError(resultVerifyEmail.Error!);
        }

        Result<UserEntity> userEntity = FactoryEntity(request);

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

        Error error = DomainErrors.UserError.DuplicateEmail;

        logger.LogAlreadyExists(nameof(CreateCommandHandler),
            nameof(ExistEmailAsync), email, error);

        return Result.FromInvalid(error);
    }
    
    private async Task<bool> ExistEmail2Async(
        string email,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        bool existsEmail = await emailReadRepository
            .ExistsAsync(email, cancellationToken);

        if (!existsEmail)
        {
            return true;
        }

        Error error = DomainErrors.UserError.DuplicateEmail;

        logger.LogAlreadyExists(nameof(CreateCommandHandler),
            nameof(ExistEmailAsync), email, error);

        return false;
    }

    private Result<UserEntity> FactoryEntity(
        CreateCommand request)
    {
        using Activity? activity = ActivitySource.StartActivity();

        Result<UserEntity> entity = UserEntity.Create(
            request.FullName, request.Birthday!.Value,
            request.Gender!.Value, request.Document);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(CreateCommandHandler),
            nameof(FactoryEntity), entity.Errors!.Count());

        return Result<UserEntity>
            .FromInvalid(entity.Errors!);
    }

    private Result<EmailEntity> FactoryEmailEntity(
        CreateCommand request, UserEntity user)
    {
        using Activity? activity = ActivitySource.StartActivity();

        Result<EmailEntity> entity = EmailEntity
            .Create(user.Id, request.Email);

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
        UserEntity user, EmailEntity email)
    {
        using Activity? activity = ActivitySource.StartActivity();

        var content = new
        {
            UserId = user.Id,
            Email = email.Email.Value
        };

        string jsonContent = JsonSerializer.Serialize(content);

        OutboxMessageEntity entity = OutboxMessageEntity
            .Create(nameof(UserEntity), jsonContent);

        return entity;
    }

    private async Task CreateAsync(
        UserEntity userEntity,
        EmailEntity emailEntity,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity();

        OutboxMessageEntity message =
            FactoryOutboxMessageEntity(userEntity, emailEntity);

        try
        {
            await userWriteRepository.CreateAsync(
                userEntity, cancellationToken);

            await emailWriteRepository.CreateAsync(
                emailEntity, cancellationToken);

            await outboxWriteRepository
                .CreateAsync(message, cancellationToken);

            await unitOfWork.CommitAsync(
                cancellationToken);

            logger.LogCreated(nameof(CreateCommandHandler),
                nameof(CreateAsync), userEntity!.Id);
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