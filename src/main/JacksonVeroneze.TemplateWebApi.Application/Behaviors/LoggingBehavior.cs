using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

namespace JacksonVeroneze.TemplateWebApi.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : BaseResponse
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(
        ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("{class} - {request} - {response} - Handling",
            nameof(LoggingBehavior<TRequest, TResponse>),
            nameof(TRequest),
            nameof(TResponse));

        TResponse response = await next();

        _logger.LogInformation("{class} - {request} - {response} - Handled",
            nameof(LoggingBehavior<TRequest, TResponse>),
            nameof(TRequest),
            nameof(TResponse));

        return response;
    }
}