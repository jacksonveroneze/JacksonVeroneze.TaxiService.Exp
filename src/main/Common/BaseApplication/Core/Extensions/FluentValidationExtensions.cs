using JacksonVeroneze.NET.Result;

namespace TaxiService.Common.BaseApplication.Core.Extensions;

public static class FluentValidationExtensions
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