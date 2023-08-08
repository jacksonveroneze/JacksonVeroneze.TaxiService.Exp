namespace JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

public record BadRequestResponse : BaseResponse
{
    public BadRequestResponse(string key, string message)
        : base(ResponseStatus.BadRequest, new Notification(key, message))
    {
    }

    public BadRequestResponse(ICollection<Notification> messages)
        : base(ResponseStatus.BadRequest, messages)
    {
    }
}
