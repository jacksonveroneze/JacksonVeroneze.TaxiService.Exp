namespace JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

public abstract class OkResponse : BaseResponse
{
    public OkResponse() : base(ResponseStatus.Ok)
    {
    }
}