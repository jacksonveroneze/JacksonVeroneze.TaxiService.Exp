using System.Text.Json;
using JacksonVeroneze.NET.EntityFramework.Interfaces;
using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.Repositories.Outbox;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.User;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.User;

public sealed class CreateUserCommandHandler(
    ILogger<CreateUserCommandHandler> logger,
    IMapper mapper,
    IUserWriteRepository userWriteRepository,
    IEmailReadRepository emailReadRepository,
    IEmailWriteRepository emailWriteRepository,
    IOutboxWriteRepository outboxWriteRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateUserCommand, Result<CreateUserCommandResponse>>
{
    public async Task<Result<CreateUserCommandResponse>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result resultVerifyEmail = await ExistEmailAsync(
            request.Email!, cancellationToken);

        if (resultVerifyEmail.IsFailure)
        {
            return Result<CreateUserCommandResponse>
                .WithError(resultVerifyEmail.Error!);
        }

        Result<UserEntity> userEntity = FactoryUserEntity(request);

        if (userEntity.IsFailure)
        {
            return Result<CreateUserCommandResponse>
                .FromInvalid(userEntity.Errors!);
        }

        Result<EmailEntity> emailEntity = FactoryEmailEntity(
            request, userEntity.Value!);

        if (emailEntity.IsFailure)
        {
            return Result<CreateUserCommandResponse>
                .FromInvalid(userEntity.Errors!);
        }

        await CreateAsync(userEntity.Value!,
            emailEntity.Value!, cancellationToken);

        CreateUserCommandResponse response =
            mapper.Map<CreateUserCommandResponse>(userEntity.Value);

        logger.LogCreated(nameof(CreateUserCommandHandler),
            nameof(Handle), userEntity.Value!.Id);

        return Result<CreateUserCommandResponse>.WithSuccess(response);
    }

    private async Task<Result> ExistEmailAsync(
        string email,
        CancellationToken cancellationToken)
    {
        bool existsUser = await emailReadRepository
            .ExistsAsync(email, cancellationToken);

        if (!existsUser)
        {
            return Result.WithSuccess();
        }

        logger.LogAlreadyExists(nameof(CreateUserCommandHandler),
            nameof(Handle), email,
            DomainErrors.UserError.DuplicateEmail);

        return Result.FromInvalid(
            DomainErrors.UserError.DuplicateEmail);
    }

    private Result<UserEntity> FactoryUserEntity(
        CreateUserCommand request)
    {
        Result<UserEntity> entity = UserEntity.Create(
            request.Name, request.Birthday!.Value,
            request.Gender!.Value, request.Document);

        if (!entity.IsFailure)
        {
            return entity;
        }

        logger.LogGenericError(nameof(CreateUserCommandHandler),
            nameof(Handle), entity.Errors!.Count());

        return Result<UserEntity>
            .FromInvalid(entity.Errors!);
    }

    private Result<EmailEntity> FactoryEmailEntity(
        CreateUserCommand request, UserEntity user)
    {
        Result<EmailEntity> entity = EmailEntity
            .Create(user.Id, request.Email);

        if (!entity.IsFailure)
        {
            return entity;
        }

        logger.LogGenericError(nameof(CreateUserCommandHandler),
            nameof(Handle), entity.Error!.Message);

        return Result<EmailEntity>
            .FromInvalid(entity.Errors!);
    }

    private static OutboxMessageEntity FactoryOutboxMessageEntity(
        UserEntity user, EmailEntity email)
    {
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

            logger.LogCreated(
                nameof(CreateUserCommandHandler),
                nameof(Handle), emailEntity.Id);
        }
        catch (Exception ex)
        {
            logger.LogGenericError(
                nameof(CreateUserCommandHandler),
                nameof(Handle), ex.Message);

            throw;
        }
    }
}