using JacksonVeroneze.NET.Result;

namespace TaxiService.Modules.Rides.Application.Core.Extensions;

internal static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule, Error error)
    {
        ArgumentNullException.ThrowIfNull(error);

        return rule
            .WithErrorCode(error.Code)
            .WithMessage(error.Message);
    }
}