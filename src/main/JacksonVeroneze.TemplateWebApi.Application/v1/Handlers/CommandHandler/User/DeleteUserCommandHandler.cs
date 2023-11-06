using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.User;

public sealed class DeleteUserCommandHandler :
    IRequestHandler<DeleteUserCommand, IResult<VoidResponse>>
{
    private readonly ILogger<DeleteUserCommandHandler> _logger;
    private readonly IDeleteUserService _service;

    public DeleteUserCommandHandler(
        ILogger<DeleteUserCommandHandler> logger,
        IDeleteUserService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task<IResult<VoidResponse>> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        IResult<VoidResponse> result = await _service
            .DeleteAsync(request.Id, cancellationToken);

        _logger.LogProcessed(nameof(ActivateUserCommandHandler),
            nameof(Handle), request.Id);

        return result;
    }
}
