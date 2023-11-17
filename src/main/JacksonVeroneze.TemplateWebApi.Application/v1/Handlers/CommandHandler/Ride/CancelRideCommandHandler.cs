using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.Ride;

public sealed class CancelRideCommandHandler :
    IRequestHandler<CancelRideCommand, IResult<VoidResponse>>
{
    private readonly ILogger<CancelRideCommandHandler> _logger;

    public CancelRideCommandHandler(
        ILogger<CancelRideCommandHandler> logger)
    {
        _logger = logger;
    }

    public Task<IResult<VoidResponse>> Handle(
        CancelRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        _logger.LogProcessed(nameof(CancelRideCommandHandler),
            nameof(Handle), request.Id);

        return Task.FromResult(
            Result<VoidResponse>.Success());
    }
}
