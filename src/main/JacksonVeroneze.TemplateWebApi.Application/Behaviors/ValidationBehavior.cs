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

    public async Task<BaseResponse> Handle(TRequest request,
        RequestHandlerDelegate<BaseResponse> next,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(next);

        if (!_validators.Any())
        {
            _logger.LogNoContainValidators(
                nameof(ValidationBehavior<TRequest, TResponse>),
                typeof(TRequest).Name);

            return await next();
        }

        IEnumerable<Task<ValidationResult>> failuresResult =
            _validators.Select(item =>
                item.ValidateAsync(request, cancellationToken));

        ValidationResult[] result =
            await Task.WhenAll(failuresResult);

        ICollection<ValidationFailure> failures = result
            .SelectMany(item => item.Errors)
            .Where(item => item != null)
            .ToArray();

        _logger.LogTotalViolations(
            nameof(ValidationBehavior<TRequest, TResponse>),
            typeof(TRequest).Name,
            failures.Count);

        if (!failures.Any())
        {
            return await next();
        }

        ICollection<Notification> fails = failures.Select(item =>
            new Notification(
                item.PropertyName,
                item.ErrorMessage)).ToList();

        return new BadRequestResponse(fails);
    }
}
