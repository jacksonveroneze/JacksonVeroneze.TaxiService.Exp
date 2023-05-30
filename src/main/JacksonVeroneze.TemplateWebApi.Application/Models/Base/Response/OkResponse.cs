namespace JacksonVeroneze.TemplateWebApi.Application.Models.Base.Response;

public abstract record OkResponse() :
    BaseResponse(ResponseStatus.Ok);
