using System.Diagnostics.CodeAnalysis;
using System.Text;
using JacksonVeroneze.NET.Result;

namespace TaxiService.Common.BaseApplication.Core.Errors;

[ExcludeFromCodeCoverage]
public static class CommonValidationErrors
{
    private static readonly CompositeFormat TemplateIsRequired =
        CompositeFormat.Parse("The {0} is required.");

    #region common

    public static class CommonErrors
    {
        internal static Error IdIsRequired =>
            new("Common.IdIsRequired",
                string.Format(null, TemplateIsRequired, "id"));
    }

    #endregion
}