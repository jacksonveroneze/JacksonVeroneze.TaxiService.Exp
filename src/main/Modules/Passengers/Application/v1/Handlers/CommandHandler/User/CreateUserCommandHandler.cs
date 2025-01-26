using JacksonVeroneze.NET.Result;
using TaxiService.Common.Application.Extensions;
using TaxiService.Common.Application.Interfaces.UnitOfWork;
using TaxiService.Modules.Passengers.Application.v1.Commands.User;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.User;
using TaxiService.Modules.Passengers.Application.v1.Models.User;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.User;

public sealed class CreateUserCommandHandler(
    ILogger<CreateUserCommandHandler> logger,
    IMapper mapper,
    IUserWriteRepository userWriteRepository,
    IEmailReadRepository emailReadRepository,
    IEmailWriteRepository emailWriteRepository,
    // IOutboxWriteRepository outboxWriteRepository,
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

        Result<UserEntity> userEntity = FactoryEntity(request);

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

        // await CreateAsync(userEntity.Value!,
        //     emailEntity.Value!, cancellationToken);

        CreateUserCommandResponse response =
            mapper.Map<CreateUserCommandResponse>(userEntity.Value);

        return Result<CreateUserCommandResponse>.WithSuccess(response);
    }

    private async Task<Result> ExistEmailAsync(
        string email,
        CancellationToken cancellationToken)
    {
        bool existsEmail = await emailReadRepository
            .ExistsAsync(email, cancellationToken);

        if (!existsEmail)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.UserError.DuplicateEmail;

        logger.LogAlreadyExists(nameof(CreateUserCommandHandler),
            nameof(ExistEmailAsync), email, error);

        return Result.FromInvalid(error);
    }

    private Result<UserEntity> FactoryEntity(
        CreateUserCommand request)
    {
        Result<UserEntity> entity = UserEntity.Create(
            request.FullName, request.Birthday!.Value,
            request.Gender!.Value, request.Document);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(CreateUserCommandHandler),
            nameof(FactoryEntity), entity.Errors!.Count());

        return Result<UserEntity>
            .FromInvalid(entity.Errors!);
    }

    private Result<EmailEntity> FactoryEmailEntity(
        CreateUserCommand request, UserEntity user)
    {
        Result<EmailEntity> entity = EmailEntity
            .Create(user.Id, request.Email);

        if (entity.IsSuccess)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(CreateUserCommandHandler),
            nameof(Handle), entity.Errors!.Count());

        return Result<EmailEntity>
            .FromInvalid(entity.Errors!);
    }

    // private static OutboxMessageEntity FactoryOutboxMessageEntity(
    //     UserEntity user, EmailEntity email)
    // {
    //     var content = new
    //     {
    //         UserId = user.Id,
    //         Email = email.Email.Value
    //     };
    //
    //     string jsonContent = JsonSerializer.Serialize(content);
    //
    //     OutboxMessageEntity entity = OutboxMessageEntity
    //         .Create(nameof(UserEntity), jsonContent);
    //
    //     return entity;
    // }
    //
    // private async Task CreateAsync(
    //     UserEntity userEntity,
    //     EmailEntity emailEntity,
    //     CancellationToken cancellationToken)
    // {
    //     OutboxMessageEntity message =
    //         FactoryOutboxMessageEntity(userEntity, emailEntity);
    //
    //     try
    //     {
    //         await userWriteRepository.CreateAsync(
    //             userEntity, cancellationToken);
    //
    //         await emailWriteRepository.CreateAsync(
    //             emailEntity, cancellationToken);
    //
    //         await outboxWriteRepository
    //             .CreateAsync(message, cancellationToken);
    //
    //         await unitOfWork.CommitAsync(
    //             cancellationToken);
    //
    //         logger.LogCreated(nameof(CreateUserCommandHandler),
    //             nameof(CreateAsync), userEntity!.Id);
    //     }
    //     catch (Exception ex)
    //     {
    //         logger.LogGenericError(
    //             nameof(CreateUserCommandHandler),
    //             nameof(CreateAsync), ex.Message);
    //
    //         throw;
    //     }
    // }
}