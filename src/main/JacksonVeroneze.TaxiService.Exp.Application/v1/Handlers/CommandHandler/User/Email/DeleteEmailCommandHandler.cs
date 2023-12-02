using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Commands.User.Email;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TaxiService.Exp.Application.v1.Models.Base;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using JacksonVeroneze.TaxiService.Exp.Domain.Core.Errors;
using JacksonVeroneze.TaxiService.Exp.Domain.Entities;

namespace JacksonVeroneze.TaxiService.Exp.Application.v1.Handlers.CommandHandler.User.Email;

public sealed class DeleteEmailCommandHandler(
    ILogger<DeleteEmailCommandHandler> logger,
    IUserReadRepository readRepository,
    IUserWriteRepository writeRepository)
    : IRequestHandler<DeleteEmailCommand, Result<VoidResponse>>
{
    public async Task<Result<VoidResponse>> Handle(
        DeleteEmailCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserEntity? entity = await readRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            logger.LogNotFound(nameof(DeleteEmailCommandHandler),
                nameof(Handle), request.Id, DomainErrors.User.NotFound);

            return Result<VoidResponse>.FromNotFound(
                DomainErrors.User.NotFound);
        }

        EmailEntity? email = entity.GetEmailById(request.EmailId);

        if (email is null)
        {
            logger.LogNotFound(nameof(DeleteEmailCommandHandler),
                nameof(Handle), request.Id, DomainErrors.Email.NotFound);

            return Result<VoidResponse>.FromNotFound(
                DomainErrors.Email.NotFound);
        }

        Result result = entity.RemoveEmail(email);

        if (result.IsFailure)
        {
            logger.LogGenericError(nameof(DeleteEmailCommandHandler),
                nameof(Handle), request.Id, result.Error!);

            return Result<VoidResponse>.FromInvalid(result.Error!);
        }

        await writeRepository.UpdateAsync(entity, cancellationToken);

        logger.LogProcessed(nameof(DeleteEmailCommandHandler),
            nameof(Handle), entity.Id);

        return Result<VoidResponse>.WithSuccess();
    }
}
