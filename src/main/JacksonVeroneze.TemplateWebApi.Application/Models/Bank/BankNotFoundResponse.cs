using System.Globalization;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

namespace JacksonVeroneze.TemplateWebApi.Application.Models.Bank;

public record BankNotFoundResponse : NotFoundResponse
{
    private const string Message = "{0} not found";
    private const string Key = "IdBank";

    protected BankNotFoundResponse()
    {
    }

    public BankNotFoundResponse(string attemptKeyValue) :
        base(Key, string.Format(CultureInfo.CurrentCulture, Message, attemptKeyValue))
    {
    }
}
