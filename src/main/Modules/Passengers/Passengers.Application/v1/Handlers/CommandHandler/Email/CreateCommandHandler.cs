using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Commands.Email;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.User;
using TaxiService.Modules.Passengers.Application.v1.Models.Email;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.Email;

public sealed class CreateCommandHandler(
    ILogger<CreateCommandHandler> logger,
    IMapper mapper,
    IUserReadRepository userReadRepository,
    IEmailReadRepository emailReadRepository,
    IEmailWriteRepository emailWriteRepository)
    : IRequestHandler<CreateCommand, Result<CreateCommandResponse>>
{
    public async Task<Result<CreateCommandResponse>> Handle(
        CreateCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<UserEntity> resultUser = await GetUserByIdAsync(
            request.UserId, cancellationToken);

        if (resultUser.IsFailure)
        {
            return Result<CreateCommandResponse>.FromNotFound(
                resultUser.Error!);
        }

        Result existsEmail = await ExistsEmailAsync(
            request, cancellationToken);

        if (existsEmail.IsFailure)
        {
            return Result<CreateCommandResponse>
                .FromInvalid(existsEmail.Error!);
        }

        Result<EmailEntity> entity = FactoryEntity(
            request, resultUser.Value!);

        if (entity.IsFailure)
        {
            return Result<CreateCommandResponse>
                .FromInvalid(entity.Errors!);
        }

        await CreateAsync(entity.Value!, cancellationToken);

        CreateCommandResponse response =
            mapper.Map<CreateCommandResponse>(entity.Value);

        return Result<CreateCommandResponse>.WithSuccess(response);
    }

    private Result<EmailEntity> FactoryEntity(
        CreateCommand request, UserEntity user)
    {
        Result<EmailEntity> entity = EmailEntity
            .Create(user.Id, request.Body!.Email);

        if (!entity.IsFailure)
        {
            return entity;
        }

        logger.LogGenericError(
            nameof(CreateCommandHandler),
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
        CreateCommand request,
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
            nameof(CreateCommandHandler),
            nameof(Handle), request.UserId, error);

        return Result<CreateCommandResponse>
            .FromInvalid(error);
    }

    private async Task CreateAsync(
        EmailEntity email,
        CancellationToken cancellationToken)
    {
        await emailWriteRepository.CreateAsync(
            email, cancellationToken);

        logger.LogProcessed(
            nameof(CreateCommandHandler),
            nameof(CreateAsync), email.Id);
    }
}