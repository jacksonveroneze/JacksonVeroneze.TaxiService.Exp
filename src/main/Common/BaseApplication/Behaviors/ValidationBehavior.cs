using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;
using TaxiService.Common.BaseApplication.Extensions;

namespace TaxiService.Common.BaseApplication.Behaviors;

[ExcludeFromCodeCoverage]
public sealed class ValidationBehavior<TRequest, TResponse>(
    ILogger<ValidationBehavior<TRequest, TResponse>> logger,
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : class
{
    private static readonly ActivitySource ActivitySource =
        new("TaxiService");

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        using Activity? activity = ActivitySource.StartActivity("ValidationBehavior");

        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(next);

        if (!validators.Any())
        {
            logger.LogNoContainValidators(nameof(ValidationBehavior<TRequest, TResponse>),
                typeof(TRequest).Name);

            return await next(cancellationToken);
        }

        ValidationContext<TRequest> context = new(request);

        ValidationResult[] validationfailures = await Task.WhenAll(
            validators.Select(validator => validator
                .ValidateAsync(context, cancellationToken)));

        ValidationFailure[] res = validationfailures
            .SelectMany(item => item.Errors)
            .ToArray();

        logger.LogTotalViolations(nameof(ValidationBehavior<TRequest, TResponse>),
            typeof(TRequest).Name,
            res.Length);

        return res.Length != 0
            ? throw new ValidationException(res)
            : await next(cancellationToken);
    }
}