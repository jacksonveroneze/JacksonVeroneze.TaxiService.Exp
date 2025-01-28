using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Common.BaseApplication.Interfaces.UnitOfWork;
using TaxiService.Modules.Passengers.Application.v1.Commands.Email;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.User;
using TaxiService.Modules.Passengers.Application.v1.Models.Email;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.Email;

public sealed class CreateEmailCommandHandler(
    ILogger<CreateEmailCommandHandler> logger,
    IMapper mapper,
    IUserReadRepository userReadRepository,
    IEmailReadRepository emailReadRepository,
    IEmailWriteRepository emailWriteRepository)
    : IRequestHandler<CreateEmailCommand, Result<CreateEmailCommandResponse>>
{
    public async Task<Result<CreateEmailCommandResponse>> Handle(
        CreateEmailCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<UserEntity> resultUser = await GetUserByIdAsync(
            request.UserId, cancellationToken);

        if (resultUser.IsFailure)
        {
            return Result<CreateEmailCommandResponse>.FromNotFound(
                resultUser.Error!);
        }

        Result existsEmail = await ExistsEmailAsync(
            request, cancellationToken);

        if (existsEmail.IsFailure)
        {
            return Result<CreateEmailCommandResponse>
                .FromInvalid(existsEmail.Error!);
        }

        Result<EmailEntity> entity = FactoryEntity(
            request, resultUser.Value!);

        if (entity.IsFailure)
        {
            return Result<CreateEmailCommandResponse>
                .FromInvalid(entity.Errors!);
        }

        await CreateAsync(entity.Value!, cancellationToken);

        CreateEmailCommandResponse response =
            mapper.Map<CreateEmailCommandResponse>(entity.Value);

        return Result<CreateEmailCommandResponse>.WithSuccess(response);
    }

    private Result<EmailEntity> FactoryEntity(
        CreateEmailCommand request, UserEntity user)
    {
        Result<EmailEntity> entity = EmailEntity
            .Create(user.Id, request.Body!.Email);

        if (!entity.IsFailure)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(CreateEmailCommandHandler),
            nameof(FactoryEntity), entity.Errors!.Count());

        return Result<EmailEntity>
            .FromInvalid(entity.Errors!);
    }

    private async Task<Result<UserEntity>> GetUserByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        UserEntity? user = await userReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (user is null)
        {
            return Result<UserEntity>.FromNotFound(
                DomainErrors.UserError.NotFound);
        }

        return Result<UserEntity>.WithSuccess(user);
    }

    private async Task<Result> ExistsEmailAsync(
        CreateEmailCommand request,
        CancellationToken cancellationToken)
    {
        bool existsEmail = await emailReadRepository
            .ExistsAsync(request.Body!.Email!, cancellationToken);

        if (!existsEmail)
        {
            return Result.WithSuccess();
        }

        Error error = DomainErrors.EmailError.DuplicateEmail;

        logger.LogGenericError(
            nameof(CreateEmailCommandHandler),
            nameof(Handle), request.UserId, error);

        return Result<CreateEmailCommandResponse>
            .FromInvalid(error);
    }

    private async Task CreateAsync(
        EmailEntity email,
        CancellationToken cancellationToken)
    {
        await emailWriteRepository.CreateAsync(
            email, cancellationToken);

        logger.LogProcessed(
            nameof(CreateEmailCommandHandler),
            nameof(CreateAsync), email.Id);
    }
}