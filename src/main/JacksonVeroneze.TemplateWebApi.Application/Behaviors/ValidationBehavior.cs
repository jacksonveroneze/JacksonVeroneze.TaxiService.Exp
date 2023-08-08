using FluentValidation.Results;
using JacksonVeroneze.TemplateWebApi.Application.Extensions;
using ValidationException = JacksonVeroneze.TemplateWebApi.Application.Exceptions.ValidationException;

namespace JacksonVeroneze.TemplateWebApi.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : class
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

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(next);

        if (!_validators.Any())
        {
            return await next();
        }

        ValidationContext<TRequest> context = new(request);

        ValidationResult[] validationfailures = await Task.WhenAll(
            _validators.Select(validator => validator
                .ValidateAsync(context, cancellationToken)));

        Dictionary<string, string[]> failures = validationfailures
            .Where(result => !result.IsValid)
            .SelectMany(item => item.Errors)
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessages) => new
                {
                    Key = propertyName, Values = errorMessages.Distinct().ToArray()
                })
            .ToDictionary(x => x.Key, x => x.Values);
        ;

        _logger.LogTotalViolations(
            nameof(ValidationBehavior<TRequest, TResponse>),
            typeof(TRequest).Name,
            failures.Count);

        if (failures.Any())
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
}
