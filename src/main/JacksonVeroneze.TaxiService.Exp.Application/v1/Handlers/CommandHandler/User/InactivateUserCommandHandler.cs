using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.Interfaces.System;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.User;

public sealed class InactivateUserCommandHandler(
    ILogger<InactivateUserCommandHandler> logger,
    IUserReadRepository readRepository,
    IUserWriteRepository writeRepository,
    IDateTime dateTime)
    : IRequestHandler<InactivateUserCommand, Result<VoidResponse>>
{
    public async Task<Result<VoidResponse>> Handle(
        InactivateUserCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserEntity? user = await readRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            return Result<VoidResponse>.FromInvalid(
                DomainErrors.User.NotFound);
        }

        Result result = user.Inactivate(dateTime.UtcNow);

        if (result.IsFailure)
        {
            logger.LogAlreadyProcessed(nameof(InactivateUserCommandHandler),
                nameof(Handle), request.Id, result.Error!);

            return Result<VoidResponse>.WithError(result.Error!);
        }

        await writeRepository.UpdateAsync(
            user, cancellationToken);

        logger.LogProcessed(nameof(InactivateUserCommandHandler),
            nameof(Handle), request.Id);

        return Result<VoidResponse>.WithSuccess();
    }
}
