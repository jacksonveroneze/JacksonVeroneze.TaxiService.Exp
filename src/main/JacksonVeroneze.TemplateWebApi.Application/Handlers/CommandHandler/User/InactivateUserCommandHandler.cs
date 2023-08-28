using JacksonVeroneze.NET.DomainObjects.Result;
using JacksonVeroneze.TemplateWebApi.Application.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Common;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.CommandHandler.User;

internal sealed class InactivateUserCommandHandler :
    IRequestHandler<InactivateUserCommand, IResult<VoidResponse>>
{
    private readonly ILogger<InactivateUserCommandHandler> _logger;
    private readonly IUserReadRepository _readRepository;
    private readonly IUserWriteRepository _writeRepository;
    private readonly IDateTime _dateTime;

    public InactivateUserCommandHandler(
        ILogger<InactivateUserCommandHandler> logger,
        IUserReadRepository readRepository,
        IUserWriteRepository writeRepository,
        IDateTime dateTime)
    {
        _logger = logger;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _dateTime = dateTime;
    }

    public async Task<IResult<VoidResponse>> Handle(
        InactivateUserCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserEntity? entity = await _readRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            _logger.LogNotFound(nameof(InactivateUserCommandHandler),
                nameof(Handle), request.Id, DomainErrors.User.NotFound);

            return Result<VoidResponse>.NotFound(
                DomainErrors.User.NotFound);
        }

        IResult result = entity.Inactivate(_dateTime.UtcNow);

        if (result.IsFailure)
        {
            _logger.AlreadyProcessed(nameof(InactivateUserCommandHandler),
                nameof(Handle), result.Error!, request.Id);

            return Result<VoidResponse>.Invalid(result.Error!);
        }

        await _writeRepository.UpdateAsync(entity, cancellationToken);

        _logger.LogProcessed(nameof(InactivateUserCommandHandler),
            nameof(Handle), request.Id);

        return Result<VoidResponse>.Success();
    }
}
