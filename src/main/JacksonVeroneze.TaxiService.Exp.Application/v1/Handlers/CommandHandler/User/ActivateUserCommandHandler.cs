using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.System;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.User;

public sealed class ActivateUserCommandHandler(
    ILogger<ActivateUserCommandHandler> logger,
    IUserReadRepository userReadRepository,
    IUserWriteRepository userWriteRepository,
    IDateTime dateTime)
    : IRequestHandler<ActivateUserCommand, Result<VoidResponse>>
{
    public async Task<Result<VoidResponse>> Handle(
        ActivateUserCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserEntity? user = await userReadRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            return Result<VoidResponse>.FromNotFound(
                DomainErrors.UserError.NotFound);
        }

        Result result = user.Activate(dateTime.UtcNow);

        if (result.IsFailure)
        {
            logger.LogAlreadyProcessed(nameof(ActivateUserCommandHandler),
                nameof(Handle), request.Id, result.Error!);

            return Result<VoidResponse>.WithError(result.Error!);
        }

        await userWriteRepository.UpdateAsync(
            user, cancellationToken);

        logger.LogProcessed(nameof(ActivateUserCommandHandler),
            nameof(Handle), request.Id);

        return Result<VoidResponse>.WithSuccess();
    }
}