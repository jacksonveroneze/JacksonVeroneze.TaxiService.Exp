using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.TemplateWebApi.Application.Commands.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.CommandHandler.User.Email;

internal sealed class DeleteEmailCommandHandler :
    IRequestHandler<DeleteEmailCommand, IResult<VoidResponse>>
{
    private readonly ILogger<DeleteEmailCommandHandler> _logger;
    private readonly IUserReadRepository _readRepository;
    private readonly IUserWriteRepository _writeRepository;

    public DeleteEmailCommandHandler(
        ILogger<DeleteEmailCommandHandler> logger,
        IUserReadRepository readRepository,
        IUserWriteRepository writeRepository)
    {
        _logger = logger;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task<IResult<VoidResponse>> Handle(
        DeleteEmailCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserEntity? entity = await _readRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            _logger.LogNotFound(nameof(DeleteEmailCommandHandler),
                nameof(Handle), request.Id, DomainErrors.User.NotFound);

            return Result<VoidResponse>.NotFound(
                DomainErrors.User.NotFound);
        }

        EmailEntity? email = entity.GetEmailById(request.EmailId);

        if (email is null)
        {
            _logger.LogNotFound(nameof(DeleteEmailCommandHandler),
                nameof(Handle), request.Id, DomainErrors.User.NotFound);

            return Result<VoidResponse>.NotFound(
                DomainErrors.User.NotFound);
        }

        IResult result = entity.RemoveEmail(email);

        if (result.IsFailure)
        {
            _logger.LogGenericError(nameof(DeleteEmailCommandHandler),
                nameof(Handle), result.Error!, request.Id.ToString());

            return Result<VoidResponse>.Invalid(result.Error!);
        }

        await _writeRepository.UpdateAsync(entity, cancellationToken);

        _logger.LogProcessed(nameof(DeleteEmailCommandHandler),
            nameof(Handle), entity.Id);

        return Result<VoidResponse>.Success();
    }
}
