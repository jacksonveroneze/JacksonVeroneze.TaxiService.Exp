namespace JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

public record NotFoundResponse : BaseResponse
{
    protected NotFoundResponse()
    {
    }

    public NotFoundResponse(string key, string message) :
        base(ResponseStatus.NotFound, FactoryNotification(key, message))
    {
    }
}
