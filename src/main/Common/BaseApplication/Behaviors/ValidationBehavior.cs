using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;
using TaxiService.Common.Application.Extensions;

namespace TaxiService.Common.Application.Behaviors;

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
            LogMessagesExtensions.LogNoContainValidators(logger, nameof(ValidationBehavior<TRequest, TResponse>),
                typeof(TRequest).Name);

            return await next();
        }

        ValidationContext<TRequest> context = new(request);

        ValidationResult[] validationfailures = await Task.WhenAll(
            validators.Select(validator => validator
                .ValidateAsync(context, cancellationToken)));

        ValidationFailure[] res = validationfailures
            .SelectMany(item => item.Errors)
            .ToArray();

        LogMessagesExtensions.LogTotalViolations(logger, nameof(ValidationBehavior<TRequest, TResponse>),
            typeof(TRequest).Name,
            res.Length);

        if (res.Any())
        {
            throw new ValidationException(res);
        }

        return await next();
    }
}