using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Services.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.User;

public sealed class InactivateUserCommandHandler :
    IRequestHandler<InactivateUserCommand, IResult<VoidResponse>>
{
    private readonly ILogger<InactivateUserCommandHandler> _logger;
    private readonly IInactivateUserService _service;

    public InactivateUserCommandHandler(
        ILogger<InactivateUserCommandHandler> logger,
        IInactivateUserService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task<IResult<VoidResponse>> Handle(
        InactivateUserCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        IResult<VoidResponse> result = await _service
            .InactivateAsync(request.Id, cancellationToken);

        _logger.LogProcessed(nameof(ActivateUserCommandHandler),
            nameof(Handle), request.Id);

        return result;
    }
}
