namespace JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

public class Notification
{
    public string Key { get; }

    public string Message { get; }

    public Notification(string key, string message)
    {
        Key = key;
        Message = message;
    }
}