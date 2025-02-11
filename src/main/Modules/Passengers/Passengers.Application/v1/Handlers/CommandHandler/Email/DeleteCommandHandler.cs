using JacksonVeroneze.NET.Result;
using TaxiService.Common.BaseApplication.Extensions;
using TaxiService.Modules.Passengers.Application.v1.Commands.Email;
using TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.Passanger;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Email;
using TaxiService.Modules.Passengers.Application.v1.Interfaces.Repositories.Passanger;
using TaxiService.Modules.Passengers.Domain.Core.Errors;
using TaxiService.Modules.Passengers.Domain.Entities;

namespace TaxiService.Modules.Passengers.Application.v1.Handlers.CommandHandler.Email;

public sealed class DeleteCommandHandler(
    ILogger<DeleteCommandHandler> logger,
    IPassangerReadRepository passangerReadRepository,
    IEmailReadRepository emailReadRepository,
    IEmailWriteRepository emailWriteRepository)
    : IRequestHandler<DeleteCommand, Result>
{
    public async Task<Result> Handle(
        DeleteCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Result<PassangerEntity> resultPassanger = await GetByIdAsync(
            request.Id, cancellationToken);

        if (resultPassanger.IsFailure)
        {
            return resultPassanger;
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

    private async Task<Result<PassangerEntity>> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        PassangerEntity? passanger = await passangerReadRepository
            .GetByIdAsync(id, cancellationToken);

        if (passanger is null)
        {
            return Result<PassangerEntity>.FromNotFound(
                DomainErrors.PassangerError.NotFound);
        }

        return Result<PassangerEntity>.WithSuccess(passanger);
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