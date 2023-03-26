using System.Collections.ObjectModel;

namespace JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

public class BaseResponse
{
    [JsonIgnore]
    public ResponseStatus Status { get; }

    [JsonPropertyName("messages")]
    public ICollection<Notification>? Messages { get; }

    public BaseResponse(ResponseStatus status)
    {
        Status = status;
    }

    public BaseResponse(ResponseStatus status,
        ICollection<Notification> messages)
    {
        Status = status;
        Messages = messages;
    }

    protected BaseResponse()
    {
    }

    public BaseResponse(ResponseStatus status,
        Notification messages)
    {
        Status = status;
        Messages = new Collection<Notification>() { messages };
    }

    protected static Notification FactoryNotification(string key,
        string message)
    {
        return new(key, message);
    }
}