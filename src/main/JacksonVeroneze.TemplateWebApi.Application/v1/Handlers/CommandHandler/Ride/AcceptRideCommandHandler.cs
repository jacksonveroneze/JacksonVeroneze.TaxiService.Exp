using JacksonVeroneze.NET.Result;
using JacksonVeroneze.TemplateWebApi.Application.v1.Commands.Ride;
using JacksonVeroneze.TemplateWebApi.Application.v1.Models.Base;

namespace JacksonVeroneze.TemplateWebApi.Application.v1.Handlers.CommandHandler.Ride;

public sealed class AcceptRideCommandHandler :
    IRequestHandler<AcceptRideCommand, IResult<VoidResponse>>
{
    private readonly ILogger<AcceptRideCommandHandler> _logger;

    public AcceptRideCommandHandler(
        ILogger<AcceptRideCommandHandler> logger)
    {
        _logger = logger;
    }

    public Task<IResult<VoidResponse>> Handle(
        AcceptRideCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        throw new NotImplementedException();
    }
}
