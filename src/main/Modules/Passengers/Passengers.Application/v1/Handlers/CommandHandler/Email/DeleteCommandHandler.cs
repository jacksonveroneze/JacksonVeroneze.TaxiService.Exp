using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Commands.Email;
using TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.User;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.User;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.Email;

public sealed class DeleteCommandHandler(
    ILogger<DeleteCommandHandler> logger,
    IUserReadRepository userReadRepository,
    IEmailReadRepository emailReadRepository,
    IEmailWriteRepository emailWriteRepository)
    : IRequestHandler<DeleteCommand, Result>
{
    public async Task<Result> Handle(
        DeleteCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<UserEntity> resultUser = await GetUserByIdAsync(
            request.Id, cancellationToken);

        if (resultUser.IsFailure)
        {
            return resultUser;
        }

        Result<EmailEntity> resultEmail = await GetEmailByIdAsync(
            request.EmailId, cancellationToken);

        if (resultEmail.IsFailure)
        {
            return resultEmail;
        }

        await DeleteAsync(resultEmail.Value!, cancellationToken);

        return Result.WithSuccess();
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

    private async Task<Result<EmailEntity>> GetEmailByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        EmailEntity? email = await emailReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (email is not null)
        {
            return Result<EmailEntity>.WithSuccess(email);
        }

        Error error = DomainErrors.EmailError.NotFound;

        logger.LogNotFound(
            nameof(DeleteCommandHandler),
            nameof(Handle), id, error);

        return Result<EmailEntity>.FromNotFound(error);
    }

    private async Task DeleteAsync(
        EmailEntity email,
        CancellationToken cancellationToken)
    {
        await emailWriteRepository.DeleteAsync(
            email, cancellationToken);

        logger.LogProcessed(
            nameof(ActivateCommandHandler),
            nameof(DeleteAsync), email.Id);
    }
}