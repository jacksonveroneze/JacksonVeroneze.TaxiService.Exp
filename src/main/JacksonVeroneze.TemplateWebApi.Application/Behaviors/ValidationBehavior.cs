using FluentValidation.Results;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

namespace JacksonVeroneze.TemplateWebApi.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, BaseResponse>
    where TRequest : IRequest<BaseResponse>
{
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(
        ILogger<ValidationBehavior<TRequest, TResponse>> logger,
        IEnumerable<IValidator<TRequest>> validators)
    {
        _logger = logger;
        _validators = validators;
    }

    public async Task<BaseResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<BaseResponse> next,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(next);

        ValidationResult[] validationfailures = await Task.WhenAll(
            _validators.Select(validator => validator
                .ValidateAsync(request, cancellationToken)));

        ICollection<Notification> failures = validationfailures
            .Where(result => !result.IsValid)
            .SelectMany(item => item.Errors)
            .Select(item => new Notification(
                item.PropertyName, item.ErrorMessage))
            .ToArray();

        _logger.LogTotalViolations(
            nameof(ValidationBehavior<TRequest, TResponse>),
            typeof(TRequest).Name,
            failures.Count);

        if (failures.Any())
        {
            return new BadRequestResponse(failures);
        }

        return await next();
    }
}
