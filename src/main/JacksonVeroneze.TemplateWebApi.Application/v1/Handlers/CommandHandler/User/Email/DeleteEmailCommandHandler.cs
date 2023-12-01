using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.v1.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.User.Email;

public sealed class DeleteEmailCommandHandler(
    ILogger<DeleteEmailCommandHandler> logger,
    IUserReadRepository readRepository,
    IUserWriteRepository writeRepository)
    : IRequestHandler<DeleteEmailCommand, IResult<VoidResponse>>
{
    public async Task<IResult<VoidResponse>> Handle(
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

            return Result<VoidResponse>.NotFound(
                DomainErrors.User.NotFound);
        }

        EmailEntity? email = entity.GetEmailById(request.EmailId);

        if (email is null)
        {
            logger.LogNotFound(nameof(DeleteEmailCommandHandler),
                nameof(Handle), request.Id, DomainErrors.Email.NotFound);

            return Result<VoidResponse>.NotFound(
                DomainErrors.Email.NotFound);
        }

        IResult result = entity.RemoveEmail(email);

        if (result.IsFailure)
        {
            logger.LogGenericError(nameof(DeleteEmailCommandHandler),
                nameof(Handle), request.Id, result.Error!);

            return Result<VoidResponse>.Invalid(result.Error!);
        }

        await writeRepository.UpdateAsync(entity, cancellationToken);

        logger.LogProcessed(nameof(DeleteEmailCommandHandler),
            nameof(Handle), entity.Id);

        return Result<VoidResponse>.Success();
    }
}
