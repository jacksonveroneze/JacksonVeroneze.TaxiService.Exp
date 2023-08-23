using JacksonVeroneze.TemplateWebApi.Application.Commands.Client;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Client;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Errors;
using JacksonVeroneze.TemplateWebApi.Domain.Core.Primitives;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;

namespace JacksonVeroneze.TemplateWebApi.Application.Handlers.CommandHandler.Client;

internal sealed  class DeleteClientCommandHandler :
    IRequestHandler<DeleteClientCommand, IResult<VoidResponse>>
{
    private readonly ILogger<DeleteClientCommandHandler> _logger;
    private readonly IClientReadRepository _readRepository;
    private readonly IClientWriteRepository _writeRepository;

    public DeleteClientCommandHandler(
        ILogger<DeleteClientCommandHandler> logger,
        IClientReadRepository readRepository,
        IClientWriteRepository writeRepository)
    {
        _logger = logger;
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task<IResult<VoidResponse>> Handle(
        DeleteClientCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        ClientEntity? data = await _readRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (data is null)
        {
            _logger.LogNotFound(nameof(DeleteClientCommandHandler),
                nameof(Handle), request.Id);

            return Result<VoidResponse>.NotFound(
                DomainErrors.Client.NotFound);
        }

        await _writeRepository.DeleteAsync(data, cancellationToken);

        _logger.LogDeleted(nameof(DeleteClientCommandHandler),
            nameof(Handle), request.Id);

        return Result<VoidResponse>.Success();
    }
}
