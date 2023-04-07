namespace JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

public record BadRequestResponse : BaseResponse
{
    public BadRequestResponse(ICollection<Notification> messages)
        : base(ResponseStatus.BadRequest, messages)
    {
    }
}