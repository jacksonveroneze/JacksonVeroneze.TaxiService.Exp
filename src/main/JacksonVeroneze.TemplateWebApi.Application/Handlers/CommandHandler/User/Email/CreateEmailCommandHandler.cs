using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.TemplateWebApi.Application.Commands.User.Email;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.CommandHandler.User.Email;

public sealed class CreateEmailCommandHandler :
    IRequestHandler<CreateEmailCommand, IResult<VoidResponse>>
{
    private readonly ILogger<CreateEmailCommandHandler> _logger;
    private readonly IUserReadRepository _readRepository;
    private readonly IUserWriteRepository _writeRepository;

    public CreateEmailCommandHandler(
        ILogger<CreateEmailCommandHandler> logger,
        IUserReadRepository readRepository,
        IUserWriteRepository writeRepository)
    {
        _logger = logger;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task<IResult<VoidResponse>> Handle(
        CreateEmailCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserEntity? entity = await _readRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            _logger.LogNotFound(nameof(CreateEmailCommandHandler),
                nameof(Handle), request.Id, DomainErrors.User.NotFound);

            return Result<VoidResponse>.NotFound(
                DomainErrors.User.NotFound);
        }

        IResult<EmailValueObject> resultEmailVo = EmailValueObject
            .Create(request.Email!);

        if (resultEmailVo.IsFailure)
        {
            _logger.LogGenericError(nameof(CreateEmailCommandHandler),
                nameof(Handle), resultEmailVo.Error!, request.Id.ToString());

            return Result<VoidResponse>.Invalid(resultEmailVo.Error!);
        }

        IResult result = entity.AddEmail(new EmailEntity(resultEmailVo.Value!));

        if (result.IsFailure)
        {
            _logger.LogGenericError(nameof(CreateEmailCommandHandler),
                nameof(Handle), result.Error!, request.Id.ToString());

            return Result<VoidResponse>.Invalid(result.Error!);
        }

        await _writeRepository.UpdateAsync(entity, cancellationToken);

        _logger.LogProcessed(nameof(CreateEmailCommandHandler),
            nameof(Handle), entity.Id);

        return Result<VoidResponse>.Success();
    }
}
