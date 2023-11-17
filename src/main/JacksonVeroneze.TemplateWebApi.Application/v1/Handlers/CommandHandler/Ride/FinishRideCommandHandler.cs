using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.Ride;

public sealed class FinishRideCommandHandler :
    IRequestHandler<FinishRideCommand, IResult<VoidResponse>>
{
    private readonly ILogger<FinishRideCommandHandler> _logger;

    public FinishRideCommandHandler(
        ILogger<FinishRideCommandHandler> logger)
    {
        _logger = logger;
    }

    public Task<IResult<VoidResponse>> Handle(
        FinishRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        _logger.LogProcessed(nameof(FinishRideCommandHandler),
            nameof(Handle), request.Id);

        return Task.FromResult(
            Result<VoidResponse>.Success());
    }
}
