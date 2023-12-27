using System.Diagnostics.CodeAnalysis;
using System.Text;
using JacksonVeroneze.NET.Result;

namespace JacksonVeroneze.TaxiService.Exp.Application.Core.Errors;

[ExcludeFromCodeCoverage]
internal static class ValidationErrors
{
    private static readonly CompositeFormat TemplateIsRequired =
        CompositeFormat.Parse("The {{0}} is required.");

    #region common

    internal static class Common
    {
        internal static Error IdIsRequired =>
            new("Common.IdIsRequired",
                string.Format(null, TemplateIsRequired, "id"));
    }

    #endregion

    #region user

    internal static class User
    {
        internal static Error IdIsRequired =>
            new("User.IdIsRequired",
                string.Format(null, TemplateIsRequired, "id"));

        internal static Error NameIsRequired =>
            new("User.NameIsRequired",
                string.Format(null, TemplateIsRequired, "name"));

        internal static Error BirthdayIsRequired =>
            new("User.BirthdayIsRequired",
                string.Format(null, TemplateIsRequired, "birthday"));

        internal static Error GenderIsRequired =>
            new("User.GenderIsRequired",
                string.Format(null, TemplateIsRequired, "gender"));

        internal static Error EmailIsRequired =>
            new("User.EmailIsRequired",
                string.Format(null, TemplateIsRequired, "e-mail"));

        internal static Error DocumentIsRequired =>
            new("User.DocumentIsRequired",
                string.Format(null, TemplateIsRequired, "document"));

        internal static Error DocumentIsInvalid =>
            new("User.DocumentIsInvalid",
                "The document is invalid.");
    }

    #endregion
}