using System.Globalization;
using JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

namespace JacksonVeroneze.TemplateWebApi.Application.Models.State;

public record StateNotFoundResponse : NotFoundResponse
{
    private const string Message = "{0} not found";
    private const string Key = "IdState";

    protected StateNotFoundResponse()
    {
    }

    public StateNotFoundResponse(string attemptKeyValue) :
        base(Key, string.Format(CultureInfo.CurrentCulture, Message, attemptKeyValue))
    {
    }
}
