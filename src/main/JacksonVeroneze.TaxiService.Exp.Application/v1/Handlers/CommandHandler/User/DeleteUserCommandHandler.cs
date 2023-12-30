using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.User;

public sealed class DeleteUserCommandHandler(
    ILogger<DeleteUserCommandHandler> logger,
    IUserReadRepository readRepository,
    IUserWriteRepository writeRepository)
    : IRequestHandler<DeleteUserCommand, Result<VoidResponse>>
{
    public async Task<Result<VoidResponse>> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserEntity? user = await readRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
        {
            return Result<VoidResponse>.FromNotFound(
                DomainErrors.UserError.NotFound);
        }

        await writeRepository.DeleteAsync(
            user, cancellationToken);

        logger.LogDeleted(nameof(DeleteUserCommandHandler),
            nameof(Handle), request.Id);

        return Result<VoidResponse>.WithSuccess();
    }
}