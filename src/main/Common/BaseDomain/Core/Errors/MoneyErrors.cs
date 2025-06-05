using JacksonVeroneze.NET.Result;

namespace TaxiService.Common.BaseDomain.Core.Errors;

public static partial class CommonDomainErrors
{
    internal static partial class MoneyError
    {
        public static Error InvalidValue =>
            Error.Create("Money.InvalidValue", "Valor inválido");
    }
}
