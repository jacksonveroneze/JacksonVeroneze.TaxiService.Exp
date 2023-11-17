using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.Ride;

public sealed class StartRideCommandHandler :
    IRequestHandler<StartRideCommand, IResult<VoidResponse>>
{
    private readonly ILogger<StartRideCommandHandler> _logger;

    public StartRideCommandHandler(
        ILogger<StartRideCommandHandler> logger)
    {
        _logger = logger;
    }

    public Task<IResult<VoidResponse>> Handle(
        StartRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        _logger.LogProcessed(nameof(StartRideCommandHandler),
            nameof(Handle), request.Id);

        return Task.FromResult(
            Result<VoidResponse>.Success());
    }
}
