using FluentValidation.Results;

using JacksonVeroneze.NET.Result;

namespace TaxiService.Common.BaseApplication.Validators;

public static class ValidateHelper
{
    public static (bool HasErrors, Error[] Errors) ValidateRequest<TRequest>(
        TRequest request,
        IEnumerable<IValidator<TRequest>> validators)
    {
        ValidationContext<TRequest> context = new(request);

        IEnumerable<ValidationResult> validationResults =
            validators.Select(v => v.Validate(context));

        ValidationFailure[] failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToArray();

        Error[] errors = failures
            .Select(failure => Error.Create(
                failure.ErrorCode, 
                failure.ErrorMessage, 
                failure.PropertyName.ToLowerInvariant()))
            .ToArray();

        return (errors.Length >= 1, errors);
    }
}
