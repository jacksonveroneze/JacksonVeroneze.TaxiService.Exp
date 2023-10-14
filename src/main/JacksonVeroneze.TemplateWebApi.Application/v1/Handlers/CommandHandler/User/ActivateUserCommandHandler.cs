using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.User;

public sealed class ActivateUserCommandHandler :
    IRequestHandler<ActivateUserCommand, IResult<VoidResponse>>
{
    private readonly ILogger<ActivateUserCommandHandler> _logger;
    private readonly IActivateUserService _service;

    public ActivateUserCommandHandler(
        ILogger<ActivateUserCommandHandler> logger,
        IActivateUserService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task<IResult<VoidResponse>> Handle(
        ActivateUserCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        IResult<VoidResponse> result = await _service
            .ActivateAsync(request.Id, cancellationToken);

        _logger.LogProcessed(nameof(ActivateUserCommandHandler),
            nameof(Handle), request.Id);

        return result;
    }
}
