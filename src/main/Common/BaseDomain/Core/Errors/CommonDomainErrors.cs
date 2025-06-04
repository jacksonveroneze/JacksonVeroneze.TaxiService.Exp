using System.Diagnostics.CodeAnalysis;
using JacksonVeroneze.NET.Result;

namespace TaxiService.Common.BaseDomain.Core.Errors;

[ExcludeFromCodeCoverage]
public static class CommonDomainErrors
{
    public const string TemplateNotFound = "The {0} with the specified identifier was not found.";
    public const string TemplateDataInUse = "The specified {0} is already in use.";
    public const string TemplateDataInvalid = "The specified {0} is invalid.";

    internal static class CpfError
    {
        public static Error InvalidCpf =>
            Error.Create("Cpf.InvalidCpf",
                string.Format(TemplateDataInvalid, "cpf"));
    }

    internal static class NameError
    {
        public static Error InvalidName =>
            Error.Create("Name.InvalidName",
                string.Format(TemplateDataInvalid, "name"));
    }

    internal static class PhoneError
    {
        public static Error InvalidPhone =>
            Error.Create("Phone.InvalidPhone",
                string.Format(TemplateDataInvalid, "phone"));
    }

    internal static class EmailError
    {
        public static Error InvalidEmail =>
            Error.Create("Email.InvalidEmail",
                string.Format(TemplateDataInvalid, "e-mail"));
    }

    internal static class CoordinateError
    {
        public static Error InvalidCoordinate =>
            Error.Create("Coordinate.InvalidCoordinate",
                string.Format(TemplateDataInvalid, "coordinate"));
    }

    internal static class MoneyError
    {
        public static Error InvalidValue =>
            Error.Create("Money.InvalidValue", "Valor inválido");
    }
}
