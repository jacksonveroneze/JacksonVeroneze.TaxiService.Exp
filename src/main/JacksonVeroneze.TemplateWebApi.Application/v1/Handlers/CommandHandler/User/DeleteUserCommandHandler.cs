using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.User;

public sealed class DeleteUserCommandHandler :
    IRequestHandler<DeleteUserCommand, IResult<VoidResponse>>
{
    private readonly ILogger<DeleteUserCommandHandler> _logger;
    private readonly IUserReadRepository _readRepository;
    private readonly IUserWriteRepository _writeRepository;

    public DeleteUserCommandHandler(
        ILogger<DeleteUserCommandHandler> logger,
        IUserReadRepository readRepository,
        IUserWriteRepository writeRepository)
    {
        _logger = logger;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task<IResult<VoidResponse>> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        UserEntity? entity = await _readRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            _logger.LogNotFound(nameof(DeleteUserCommandHandler),
                nameof(Handle), request.Id, DomainErrors.User.NotFound);

            return Result<VoidResponse>.NotFound(
                DomainErrors.User.NotFound);
        }

        await _writeRepository.DeleteAsync(entity, cancellationToken);

        _logger.LogDeleted(nameof(DeleteUserCommandHandler),
            nameof(Handle), request.Id);

        return Result<VoidResponse>.Success();
    }
}