using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Ride;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.Ride;

public sealed class RequestRideCommandHandler :
    IRequestHandler<RequestRideCommand, IResult<RequestRideCommandResponse>>
{
    private readonly ILogger<RequestRideCommandHandler> _logger;

    public RequestRideCommandHandler(
        ILogger<RequestRideCommandHandler> logger)
    {
        _logger = logger;
    }

    public Task<IResult<RequestRideCommandResponse>> Handle(
        RequestRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        throw new NotImplementedException();
    }
}
