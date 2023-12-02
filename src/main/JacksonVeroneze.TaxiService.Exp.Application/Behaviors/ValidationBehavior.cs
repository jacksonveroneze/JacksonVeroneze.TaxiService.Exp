using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;
using Exceptions_ValidationException = JacksonVeroneze.TaxiService.Exp.Application.Exceptions.ValidationException;
using ValidationException = JacksonVeroneze.TaxiService.Exp.Application.Exceptions.ValidationException;

namespace JacksonVeroneze.TaxiService.Exp.Application.Behaviors;

[ExcludeFromCodeCoverage]
public sealed class ValidationBehavior<TRequest, TResponse>(
    ILogger<ValidationBehavior<TRequest, TResponse>> logger,
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : class
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(next);

        if (!validators.Any())
        {
            logger.LogNoContainValidators(
                nameof(ValidationBehavior<TRequest, TResponse>),
                typeof(TRequest).Name);

            return await next();
        }

        ValidationContext<TRequest> context = new(request);

        ValidationResult[] validationfailures = await Task.WhenAll(
            validators.Select(validator => validator
                .ValidateAsync(context, cancellationToken)));

        Dictionary<string, string[]> failures = validationfailures
            .Where(result => !result.IsValid)
            .SelectMany(item => item.Errors)
            .GroupBy(
                p => p.PropertyName,
                e => e.ErrorMessage,
                (propertyName, errorMessages) => new
                {
                    Key = propertyName,
                    Values = errorMessages
                        .Distinct().ToArray()
                })
            .ToDictionary(k => k.Key, v => v.Values);

        logger.LogTotalViolations(
            nameof(ValidationBehavior<TRequest, TResponse>),
            typeof(TRequest).Name,
            failures.Count);

        if (failures.Any())
        {
            throw new Exceptions_ValidationException(failures);
        }

        return await next();
    }
}
