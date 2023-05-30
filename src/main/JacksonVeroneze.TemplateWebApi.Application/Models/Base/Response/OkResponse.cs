namespace JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

public abstract record OkResponse : BaseResponse
{
    public OkResponse() : base(ResponseStatus.Ok)
    {
    }
}
