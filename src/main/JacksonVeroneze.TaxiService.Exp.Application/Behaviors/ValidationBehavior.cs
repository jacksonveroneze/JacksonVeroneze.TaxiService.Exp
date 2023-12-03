using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;
using JacksonVeroneze.TaxiService.Exp.Application.Extensions;

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

        ValidationFailure[] res = validationfailures
            .SelectMany(item => item.Errors)
            .ToArray();

        logger.LogTotalViolations(
            nameof(ValidationBehavior<TRequest, TResponse>),
            typeof(TRequest).Name,
            res.Length);

        if (res.Any())
        {
            throw new ValidationException(res);
        }

        return await next();
    }
}
